using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;
    void Awake()
{
        gameObject.AddComponent<AudioSource>();
        GetComponent<AudioSource>().clip = _clips[0];
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop=true;
        GetComponent<AudioSource>().volume=0.2f;
        DontDestroyOnLoad(gameObject);
    }
    void OnLevelWasLoaded()
    {
        
        if(FindObjectsOfType<AudioSource>().Length>1)
        {
        Destroy(FindObjectsOfType<AudioSource>()[0].gameObject);
        }
        if(SceneManager.GetActiveScene()==SceneManager.GetSceneByBuildIndex(1))
        {
            GetComponent<AudioSource>().clip = _clips[1];
            GetComponent<AudioSource>().volume=1f;
            GetComponent<AudioSource>().loop=true;
            GetComponent<AudioSource>().Play();
        }
        if(SceneManager.GetActiveScene()==SceneManager.GetSceneByBuildIndex(3))
        {
            GetComponent<AudioSource>().clip = _clips[0];
            GetComponent<AudioSource>().volume=0.2f;
            GetComponent<AudioSource>().Play();
        }
        if(SceneManager.GetActiveScene()==SceneManager.GetSceneByBuildIndex(2))
        {
            GetComponent<AudioSource>().clip = _clips[0];
            GetComponent<AudioSource>().volume=0.2f;
            GetComponent<AudioSource>().pitch=0.2f;
            GetComponent<AudioSource>().Play();
        }
        if(SceneManager.GetActiveScene()==SceneManager.GetSceneByBuildIndex(0))
        {
            GetComponent<AudioSource>().clip = _clips[0];
            GetComponent<AudioSource>().volume=0.2f;
            GetComponent<AudioSource>().Play();
        }
        else GetComponent<AudioSource>().pitch=1;
    }

}
