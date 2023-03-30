using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOverCanvas : MonoBehaviour
{
    public GameObject GameOver;
    public LevelLoader levelLoader;
    public string scene;
    private AudioSource gameOverAudioSource;
    private audioManager thisAudioManager;

    void Start()
    {
        gameOverAudioSource = GameOver.GetComponentInChildren<AudioSource>();
        thisAudioManager = FindObjectOfType<audioManager>();
    }


    void Update()
    {
        if (GetComponent<Player>().dead == true)
        {
            StartCoroutine(deathCoroutine());
        }
    }

    public void Restart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        levelLoader.cursor = false;
        levelLoader.Transition(scene);
    }

    public void Menu()
    {
        levelLoader.cursor = true;
        Time.timeScale = 1f;
        levelLoader.Transition("Menu");
    }
    IEnumerator deathCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        GameOver.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GetComponent<Player>().stopSound("PlayerCaveWalking");
        thisAudioManager.stopSound("CaveSound");
        thisAudioManager.stopSound("D1 Theme");
        gameOverAudioSource.Play();
    }

}

