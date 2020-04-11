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


    public GameObject UIfunctions;

    private float timer;
    private bool playClicked;
    private bool returnClicked;


    private void Update()
    {
        if (playClicked)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 3)
        {
            StartGame();
            timer = 0;
        }
        if (returnClicked)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 3)
        {
            OpenMenu();
            timer = 0;
        }
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
        SceneManager.LoadScene("FirstLevel");
    }
    public void PlayClicked()
    {
        playClicked = true;
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

}
