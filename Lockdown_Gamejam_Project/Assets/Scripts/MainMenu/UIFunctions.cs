﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIFunctions : MonoBehaviour
{
    public Animator textAnimator;
    public Animator ladaAnimator;
    public Animator playerAnimator;
    public Animator vignetteAnimator;

    public GameObject VictoryScreen;
    public GameObject ScoreScreen;



    public GameObject UIfunctions;

    private float timer;
    private bool playClicked;
    private bool difficultyClicked;

    private bool returnClicked;
    private bool exitClicked;



    private void Update()
    {
        if (returnClicked)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 4.5f)
        {
            OpenMenu();
            timer = 0;
            returnClicked = false;
        }
        if (playClicked)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 4.5f)
        {
            StartGame();
            timer = 0;
            playClicked = false;
        }
        if (difficultyClicked)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 4.5f)
        {
            PlayWithDifficulty();
            timer = 0;
            playClicked = false;
        }
        if (exitClicked)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 4.5f)
        {
            ExitGame();
            timer = 0;
            exitClicked = false;
        }
    }

    private void PlayWithDifficulty()
    {
        SceneManager.LoadScene("FirstLevel 1");
    }

    public void TriggerCameraShake()
    {
        textAnimator.SetTrigger("ScreenShake");
    }

    public void ResetTriggerCameraShake()
    {
        textAnimator.SetTrigger("StopScreenShake");

    }
    public void SetSnareTrigger()
    {
        textAnimator.SetTrigger("SnareTrigger");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("DifficultyScreen");
    }
    public void PlayClicked()
    {
        playClicked = true;
        ladaAnimator.SetTrigger("PlayPressed");
        playerAnimator.SetTrigger("PlayPressed");
        vignetteAnimator.SetTrigger("PlayPressed");
    }
    public void DifficultyClicked()
    {
        difficultyClicked = true;
        ladaAnimator.SetTrigger("PlayPressed");
        playerAnimator.SetTrigger("PlayPressed");
        vignetteAnimator.SetTrigger("PlayPressed");
    }
    public void ReturnClicked()
    {
        returnClicked = true;
        vignetteAnimator.SetTrigger("PlayPressed");
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OpenScore()
    {
        ScoreScreen.SetActive(true);
        VictoryScreen.SetActive(false);

    }
    public void ExitClicked()
    {
        exitClicked = true;

        vignetteAnimator.SetTrigger("PlayPressed");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
