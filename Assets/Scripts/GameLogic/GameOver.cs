using System;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    private void Start()
    {
        _gameOverPanel.SetActive(false);
        PlayerMoney.ResetComboMoney();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("totalScore", PlayerMoney.TotalScore);
            _gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            GameManager.GameOver = true;
        }
    }
}