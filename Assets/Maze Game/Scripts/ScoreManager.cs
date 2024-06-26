using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int score;
    // Start is called before the first frame update
    void Start()
    {
      scoreText.text = "Score: " + score;  
    }
public void IncreaseScore()
{
    score++;
    scoreText.text = "Score: " + score;
}
}
