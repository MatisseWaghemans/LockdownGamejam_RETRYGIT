using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoteBooth : MonoBehaviour
{
    private PlayerMovement _player;
    private ScoreManager _scoreManager;
    public static int removeAmount = 2;

    private GameObject _flock;

    private bool _hasEntered = false;
    private int remove;

    [SerializeField] private bool _isEnd = false;
    private void Start()
    {
        _player = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        _scoreManager = FindObjectOfType<ScoreManager>();

        _flock = FindObjectOfType<Flock>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_hasEntered)
        {
            if (other.CompareTag("Player"))
            {
                int count = _flock.transform.childCount-1;
                Debug.Log(count);
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
                        //RemoveFollowers(count);
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
        for (int followerIndex = 0; followerIndex <= removeAmount; followerIndex++)
        {
            GameObject follower = _flock.transform.GetChild(followerIndex).gameObject;
            Flock flock = _flock.GetComponent<Flock>();
            flock.agents.Remove(flock.agents[followerIndex]);
            Destroy(follower);

        }
        Flock newFlock = _flock.GetComponent<Flock>();
        newFlock.CheckAgents();
    }
}
