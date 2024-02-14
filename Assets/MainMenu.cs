using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneManager sceneManager;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
