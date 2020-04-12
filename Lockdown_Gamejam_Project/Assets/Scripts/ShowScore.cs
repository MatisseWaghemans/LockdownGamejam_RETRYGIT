using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    private ScoreManager _scoreManager;
    private Text _text;
    void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
        _text = this.GetComponent<Text>();
        _text.text = _scoreManager.Score.ToString();

        _scoreManager.Reset();
    }
}
