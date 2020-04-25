using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for all application actions such as pausing the game, or speeding/slowing the game speed
/// </summary>
public class ApplicationActions : MonoBehaviour
{
    public Text gameSpeedText;
    
    private bool isPaused = false;
    private float gameSpeed = 1f;
    private float currentGameSpeed = 1f;
    private float maxGameSpeed = 3f;
    
    public static ApplicationActions instance;
    
    /// <summary>
    /// Default method, makes sure that only one instance of ApplicationActions class is created (singleton)
    /// </summary>
    void Awake ()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    /// <summary>
    /// Default method to update the game constantly
    /// </summary>
    void Update()
    {
        Time.timeScale = gameSpeed;
        updateGameSpeedText();
    }
    
    /// <summary>
    /// Handles click on pause button
    /// </summary>
    public void handlePauseClick()
    {
        playButtonClickSound();
        if (!isPaused)
        {
            pause();
        }
        else
        {
            unpause();
        }
        isPaused = !isPaused;
    }

    /// <summary>
    /// Handles click on fastforward button
    /// </summary>
    public void handleFastforwardClick()
    {
        playButtonClickSound();
        if (!isPaused)
        {
            currentGameSpeed++;
            if (currentGameSpeed > maxGameSpeed)
            {
                currentGameSpeed = 1f;
            }

            gameSpeed = currentGameSpeed;   
        }
    }

    /// <summary>
    /// Pauses the game, that is stops all actions that er dependent on time
    /// </summary>
    public void pause()
    {
        gameSpeed = 0;
    }

    /// <summary>
    /// Unpause the game, all actions that are dependent on time will run again
    /// </summary>
    public void unpause()
    {
        gameSpeed = currentGameSpeed;
    }

    /// <summary>
    /// Updates the game speed text telling the user the actual game speed
    /// </summary>
    private void updateGameSpeedText()
    {
        gameSpeedText.text = "Game speed: " + currentGameSpeed + "x";
    }
    
    /// <summary>
    /// Plays button click sound
    /// </summary>
    private void playButtonClickSound()
    {
        FindObjectOfType<AudioManager>().play("ButtonClick");
    }
}
