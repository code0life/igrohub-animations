using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public long score = 0;

    Text label;
    
    void Start()
    {
        label = GetComponent<Text>();
        UpdateScore();
    }

    public void Add(int v)
    {
        score += v;
        UpdateScore();
    }

    void UpdateScore()
    {
        label.text = $"{score} point(s)";
    }
}
