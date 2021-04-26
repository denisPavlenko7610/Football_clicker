using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private Text comboText;
    public static int TotalScore { get; set; }
    public static int ComboCount { get; set; }

    private bool isCombo2X;
    private bool isCombo3X;
    const int combo2X = 5;
    const int combo3X = 10;


    private void Start()
    {
        TotalScore = PlayerPrefs.GetInt("totalScore");
    }

    private void Update()
    {
        CountComboMoney();
    }

    private void CountComboMoney()
    {
        if (IsCombo2X())
        {
            comboText.text = "2x Combo";
        }
        else if (IsCombo3X())
        {
            comboText.text = "3x Combo";
        }
        else
        {
            comboText.text = "";
        }
    }

    public static void SetComboMoney()
    {
        if (IsCombo2X())
        {
            TotalScore += combo2X;
        }
        else if (IsCombo3X())
        {
            TotalScore += combo3X;
        }
    }

    public static void ResetComboMoney()
    {
        ComboCount = 0;
    }

    private static bool IsCombo2X()
    {
        if (ComboCount >= combo2X && ComboCount < combo3X)
        {
            return true;
        }

        return false;
    }

    private static bool IsCombo3X()
    {
        if (ComboCount >= combo3X)
        {
            return true;
        }

        return false;
    }
}