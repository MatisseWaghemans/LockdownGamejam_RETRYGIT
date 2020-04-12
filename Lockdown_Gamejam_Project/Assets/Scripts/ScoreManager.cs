using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _score = 0;
    public int Score { get => _score; set => _score = value; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void AddScore(int amount)
    {
        _score += amount;
    }

    public void Reset()
    {
        _score = 0;
    }
}
