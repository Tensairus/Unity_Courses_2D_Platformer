using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collider;

    public CircleCollider2D Collider => _collider;

    public event Action<Coin> CoinPicked;

    public void OnCoinPicked()
    {
        CoinPicked?.Invoke(this);
    }
}
