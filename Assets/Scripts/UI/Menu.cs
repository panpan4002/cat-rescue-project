using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public LevelLoader levelLoader;
    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        levelLoader.cursor = false;
        levelLoader.Transition("L1");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
       //UnityEditor.EditorApplication.isPlaying = false;
       Application.Quit();
    }
}
