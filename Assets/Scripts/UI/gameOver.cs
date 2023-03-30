using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameOver : MonoBehaviour
{
    public GameObject GameOver;
    public LevelLoader levelLoader;
    public string scene;

    void Start()
    {

    }


    void Update()
    {
        if (GetComponent<Player>().dead == true)
        {
            GameOver.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Restart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GetComponent<Pause>().isPaused = false;
        SceneManager.LoadScene(scene);
    }

    public void Menu()
    {
        levelLoader.Transition("Menu");
    }
}
