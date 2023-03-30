using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transitionAnim;
    public bool cursor;
    public gamePika gamePikaFoda;

    void Start()
    {
        gamePikaFoda = GameObject.FindGameObjectWithTag("gamePika").GetComponent<gamePika>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            transitionAnim.SetTrigger("load");
        }
    }
    public void Transition(string scene)
    {
        StartCoroutine(LoadScene(scene));

        if(cursor == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }
    IEnumerator LoadScene(string scene)
    {
        transitionAnim.SetTrigger("start");

        yield return new WaitForSecondsRealtime(1);


        SceneManager.LoadSceneAsync(scene);
    }

    public void restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void checkPointRestart()
    {
        gamePikaFoda.checkPointRespawn = true;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
