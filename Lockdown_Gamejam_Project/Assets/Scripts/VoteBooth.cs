﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoteBooth : MonoBehaviour
{
    private PlayerMovement _player;
    private ScoreManager _scoreManager;
    [SerializeField] private int removeAmount = 2;

    private GameObject _flock;

    private bool _hasEntered = false;
    private int remove;

    [SerializeField] private bool _isEnd = false;
    private void Start()
    {
        _player = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        _scoreManager = (ScoreManager)FindObjectOfType(typeof(ScoreManager));

        _flock = FindObjectOfType<Flock>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_hasEntered)
        {
            if (other.CompareTag("Player"))
            {
                int count = _flock.transform.childCount;
                if (_isEnd)
                {
                    if (count == 0)
                    {
                        SceneManager.LoadScene("VictoryScreen");
                    }
                    else
                    {
                        remove = count;
                        _scoreManager.AddScore(remove);
                        RemoveFollowers(count);
                        SceneManager.LoadScene("VictoryScreen");
                    }
                    return;
                }
                Debug.Log("Player has entered");
                if (count == 0)
                {
                    return;
                }
                if (count < removeAmount)
                {
                    remove = count;
                    RemoveFollowers(remove);
                    _scoreManager.AddScore(remove);
                }
                else
                {
                    remove = removeAmount;
                    RemoveFollowers(remove);
                    _scoreManager.AddScore(remove);
                }
                _hasEntered = true;
            }
        }      
    }

    private void RemoveFollowers(int removeAmount)
    {
        for (int followerIndex = 0; followerIndex < removeAmount; followerIndex++)
        {
            GameObject follower = _flock.transform.GetChild(followerIndex).gameObject;
            Destroy(follower);
        }
    }
}
