using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMoney.SetComboMoney();
            PlayerMoney.ResetComboMoney();
        }
    }
}