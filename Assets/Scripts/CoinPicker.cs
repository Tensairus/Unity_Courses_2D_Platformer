using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coin coin;

        if (collision.TryGetComponent<Coin>(out coin))
        {
            coin.OnCoinPicked();
        }
    }
}
