using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        text.text = "0GB";
        ScoreManager.Instance.OnScoreChange += HandleScoreChange;
    }

    private void HandleScoreChange(int score)
    {
        text.text = $"{string.Format("{0:F1}", (float)score/1000)}GB";
    }
}
