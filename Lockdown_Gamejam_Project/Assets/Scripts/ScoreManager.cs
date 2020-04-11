using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _score = 0;
    public int Score { get => _score; set => _score = value; }

    public void AddScore(int amount)
    {
        _score += amount;
        Debug.Log(_score);
    }
}
