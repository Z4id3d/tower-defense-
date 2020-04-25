using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents end of game menu, that is when the player loses. Also contains some actions to be
/// performed when the game ends
/// </summary>
public class EndOfGameMenu : MonoBehaviour
{
    public static EndOfGameMenu instance;
    void Awake ()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    
    void Start()
    {
        gameObject.SetActive(false); //Hide the menu when the game starts       
    }
    
    /// <summary>
    /// Makes the menu visible
    /// </summary>
    public void showEndOfGameMenu()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Handles replay button click
    /// </summary>
    public void handleReplayClick()
    {
        playButtonClickSound();
        resetStaticGameState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Handles to main menu button click
    /// </summary>
    public void handleToMainMenuClick()
    {
        playButtonClickSound();
        resetStaticGameState();
        SceneManager.LoadScene("Menu");
    }
    
    /// <summary>
    /// Resets the game state. That is: Resets all static variables to their default values
    /// </summary>
    private void resetStaticGameState()
    {
        //Reset house health
        HouseHealth.resetHealth();

        //Reset money
        Player.restMoney();

        //Reset enemies alive
        WaveSpawner.resetEnemiesAlive();

        //Hide end of game menu
        gameObject.SetActive(false);
        
        //Reset lost state
        Player.restLostState();
        
        //Reset game speed
        ApplicationActions.instance.unpause();
        
        //Reset node list
        Node.goList = new List<Node>();
    }
    
    /// <summary>
    /// Plays button click sound
    /// </summary>
    private void playButtonClickSound()
    {
        FindObjectOfType<AudioManager>().play("ButtonClick");
    }
}
