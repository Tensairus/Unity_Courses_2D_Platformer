using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collider;

    public CircleCollider2D Collider => _collider;

    public event Action<Coin> CoinPicked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent<Player>(out _))
        {
            CoinPicked?.Invoke(this);
        }
    }
}
