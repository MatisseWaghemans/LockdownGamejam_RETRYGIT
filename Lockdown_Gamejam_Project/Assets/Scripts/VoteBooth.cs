using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteBooth : MonoBehaviour
{
    private PlayerMovement _player;
    private ScoreManager _scoreManager;
    [SerializeField] private int removeAmount = 2;

    private bool _hasEntered = false;
    private int remove;
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
                Debug.Log("Player has entered");
                if (_player._followers.Count == 0)
                {
                    return;
                }
                if (_player._followers.Count < removeAmount)
                {
                    remove = _player._followers.Count;
                }
                else
                {
                    remove = removeAmount;                 
                }
                RemoveFollowers(remove);
                _scoreManager.AddScore(remove);
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
