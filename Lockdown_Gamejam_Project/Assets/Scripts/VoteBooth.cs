using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoteBooth : MonoBehaviour
{
    private PlayerMovement _player;
    private ScoreManager _scoreManager;
    [SerializeField] private int removeAmount = 2;

    private bool _hasEntered = false;
    private int remove;

    [SerializeField] private bool _isEnd = false;
    private void Start()
    {
        _player = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        _scoreManager = (ScoreManager)FindObjectOfType(typeof(ScoreManager));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_hasEntered)
        {
            if (other.CompareTag("Player"))
            {
                if (_isEnd)
                {
                    if (_player._followers.Count == 0)
                    {
                        SceneManager.LoadScene("VictoryScreen");
                    }
                    else
                    {
                        remove = _player._followers.Count;
                        _scoreManager.AddScore(remove);
                        RemoveFollowers(_player._followers.Count);
                        SceneManager.LoadScene("VictoryScreen");
                    }
                    return;
                }
                Debug.Log("Player has entered");
                if (_player._followers.Count == 0)
                {
                    return;
                }
                if (_player._followers.Count < removeAmount)
                {
                    remove = _player._followers.Count;
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
            GameObject follower = _player._followers[followerIndex].gameObject;
            _player._followers.Remove(follower);
            Destroy(follower);
        }
    }
}
