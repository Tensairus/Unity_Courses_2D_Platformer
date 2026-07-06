using System;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    public event Action<bool> WallNearbyStatusChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Platform>(out _))
        {
            WallNearbyStatusChanged?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Platform>(out _))
        {
            WallNearbyStatusChanged?.Invoke(false);
        }
    }
}
