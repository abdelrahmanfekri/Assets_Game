using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        quitGame();
    }

    void pauseGame()
    {
        isPaused = true;
        // show the pause menu
        // stop the game
    }

    void resumeGame()
    {
        isPaused = false;
        // hide the pause menu
        // resume the game
    }

    void restartGame()
    {
        // reload the scene
    }

    void quitGame()
    {
        // should quit when price esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
