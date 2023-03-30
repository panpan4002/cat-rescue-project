using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    public GameObject pausePanel;
    public LevelLoader levelLoader;
    private AudioSource pauseAudioSource;

    void Start()
    {
        pauseAudioSource = pausePanel.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GetComponent<Player>().dead == false && Input.GetButtonDown("Cancel"))
        {
            PauseScreen();
        }
    }

    void PauseScreen()
    {
        if(isPaused)
        {
            pausePanel.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GetComponent<Player>().resumeSound("PlayerCaveWalking");
            FindObjectOfType<audioManager>().resumeSound("CaveSound");
            FindObjectOfType<audioManager>().resumeSound("D1 Theme");
            pauseAudioSource.Stop();
        }
        else
        {
            pausePanel.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponent<Player>().pauseSound("PlayerCaveWalking");
            FindObjectOfType<audioManager>().pauseSound("CaveSound");
            FindObjectOfType<audioManager>().pauseSound("D1 Theme");
            pauseAudioSource.Play();
        }
    }

    public void pauseScreenEnable(bool enable)
    {
        pausePanel.SetActive(enable);
    }

    public void Resume()
    {
        PauseScreen();
    }

    public void optionsMenu()
    {
        
    }

    public void Menu()
    {
        levelLoader.cursor = true;
        Time.timeScale = 1f;
        levelLoader.Transition("Menu");
    }

    IEnumerator ResumeGame()
    {
        yield return new WaitForSecondsRealtime(.4f);
        PauseScreen();
    }
}
