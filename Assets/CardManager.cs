using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI PlayerScore;


    public void Initialize(string PlayerNames, int PlayerScores)
    {
        PlayerName.text = PlayerNames;
        PlayerScore.text = PlayerScores.ToString();
    }
}
