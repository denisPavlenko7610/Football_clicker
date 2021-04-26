using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameOver { get; set; }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("totalScore", PlayerMoney.TotalScore);
    }
}
