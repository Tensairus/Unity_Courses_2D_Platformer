using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private CircleCollider2D _coinPrefabCollider;
    [SerializeField] private LayerMask _spawnForbiddenCollisionLayers;
    [SerializeField] private float _spawnBoundaryXMin;
    [SerializeField] private float _spawnBoundaryXMax;
    [SerializeField] private float _spawnBoundaryYMin;
    [SerializeField] private float _spawnBoundaryYMax;
    [SerializeField] private float _spawnPositionOffsetY;
    [SerializeField] private int _coinsSpawnCount;
    [SerializeField] private float _coinsOverlapRadius;

    private ObjectPool<Coin> _coinsPool;
    private int _poolDefaultCapacity = 10;
    private int _poolMaxSize = 20;
    private int _coinsSpawnOnPickUpAmount = 1;

    private List<Coin> _activeCoins;

    private float _maxRayLength;
    private float _sqrCoinsOverlapRadius;

    private void Awake()
    {
        _activeCoins = new List<Coin>();
        _maxRayLength = _spawnBoundaryYMax - _spawnBoundaryYMin;
        _sqrCoinsOverlapRadius = _coinsOverlapRadius * _coinsOverlapRadius;

        _coinsPool = new ObjectPool<Coin>
            (
            createFunc: () => OnCreateNewPoolableObject(),
            actionOnGet: (coin) => OnGetPoolableObject(coin),
            actionOnDestroy: (coin) => OnDestroyPoolableObject(coin),
            actionOnRelease: (coin) => OnReleasePoolableObject(coin),
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        SpawnCoin(_coinsSpawnCount);
    }

    private void SpawnCoin(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Coin newCoin = _coinsPool.Get();
            newCoin.transform.position = PickRandomUnoccupiedPosition(newCoin);
        }
    }

    private Vector2 PickRandomUnoccupiedPosition(Coin coin)
    {
        int tryCounter = 1;
        int tryCounterLimit = 1000;
        bool isPicking = true;

        Vector2 newPosition = new Vector2();

        while (isPicking == true && tryCounter <= tryCounterLimit)
        {
            tryCounter++;

            newPosition = new Vector2(Random.Range(_spawnBoundaryXMin, _spawnBoundaryXMax), Random.Range(_spawnBoundaryYMin, _spawnBoundaryYMax));

            if (CheckOverlapCircle(coin.Collider, newPosition) == false)
            {
                RaycastHit2D hit = Physics2D.Raycast(newPosition, Vector2.down, _maxRayLength);                

                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent<Platform>(out _))
                    {
                        newPosition = hit.point;
                        newPosition.y += _spawnPositionOffsetY;

                        if (CheckCoinsOverlap(newPosition) == false)
                        {
                            isPicking = false;
                        }
                    }
                }
            }
        }

        return newPosition;
    }

    private bool CheckCoinsOverlap(Vector3 position)
    {
        foreach(Coin coin in _activeCoins)
        {
            if ((coin.transform.position - position).sqrMagnitude <= _sqrCoinsOverlapRadius)
            {
                return true;
            }
        }

        return false;
    }

    private Coin OnCreateNewPoolableObject()
    {
        Coin newCoin = Instantiate(_coinPrefab);
        newCoin.CoinPicked += OnCoinPicked;

        return newCoin;
    }

    private void OnGetPoolableObject(Coin coin)
    {
        coin.gameObject.SetActive(true);
        _activeCoins.Add(coin);
    }

    private void OnReleasePoolableObject(Coin coin)
    {
        coin.gameObject.SetActive(false);
        _activeCoins.Remove(coin);
    }

    private void OnDestroyPoolableObject(Coin coin)
    {
        coin.CoinPicked -= OnCoinPicked;
        Destroy(coin);
    }

    private void OnCoinPicked(Coin coin)
    {
        _coinsPool.Release(coin);
        SpawnCoin(_coinsSpawnOnPickUpAmount);
    }

    private bool CheckOverlapCircle(CircleCollider2D collider, Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, collider.radius, _spawnForbiddenCollisionLayers);

        return hit != null;
    }
}
