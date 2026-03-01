using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    public event Action<bool> GroundedStateChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Platform>(out _))
        {
            GroundedStateChanged?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Platform>(out _))
        {
            GroundedStateChanged?.Invoke(false);
        }
    }
}
