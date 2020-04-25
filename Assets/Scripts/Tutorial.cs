using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Represents the tutorial menu 
/// </summary>
public class Tutorial : MonoBehaviour
{
    public Text tutorialText;
    public GameObject infoMenu;
    public GameObject readyMenu;

    private bool isPlaying = true;
    private int currentPage = 1;
    private string[] messages; //Holds a list of messages to display to player

    void Start()
    {
        messages = new string[] {
            "Welcome to your home village!",
            "Your mission is to keep the house safe from zombies",
            "Different zombies will spawn from here",
            "You can buy turrets from the shop to your left and place them along the way",
            "You can move around the map by clicking the left mouse button and draging"
        };
        ApplicationActions.instance.pause();
        readyMenu.SetActive(false);
        infoMenu.SetActive(true);
    }

    /// <summary>
    /// Handles next button click
    /// </summary>
    public void handleNextClick()
    {
        playButtonClickSound();
        next();
    }

    /// <summary>
    /// Handles skip button click
    /// </summary>
    public void handleSkipClick()
    {
        playButtonClickSound();
        skip();
    }
    
    /// <summary>
    /// Handles ready button click
    /// </summary>
    public void handleReadyClick(){
        playButtonClickSound();
        ApplicationActions.instance.unpause();
        readyMenu.SetActive(false);
        infoMenu.SetActive(false);
        isPlaying = false;
    }
    
    /// <summary>
    /// Handles replay tutorial button click
    /// </summary>
    public void handleReplayTutorialClick()
    {
        playButtonClickSound();
        currentPage = 1;
        tutorialText.text = messages[currentPage-1];  
        readyMenu.SetActive(false);
        infoMenu.SetActive(true);
    }

    /// <summary>
    /// Skips tutorial, that is hides the tutorial menu and show ready menu
    /// </summary>
    private void skip()
    {
        infoMenu.SetActive(false);
        readyMenu.SetActive(true);
    }

    /// <summary>
    /// Goes to next slide in tutorial
    /// </summary>
    private void next()
    {
        currentPage++;
        if (currentPage <= messages.Length)
        {
            tutorialText.text = messages[currentPage-1];
            if (currentPage == 3)
            {
                Debug.Log("Move camera");
                CameraMovement.instance.moveCameraToZombieSpawn();
            }
        }
        else
        {
            skip();
        }
    }

    /// <summary>
    /// Returns if tutorial is playing or not
    /// </summary>
    public bool isPlayingTutorial()
    {
        return isPlaying;
    }
    
    /// <summary>
    /// Plays button click sound
    /// </summary>
    private void playButtonClickSound()
    {
        FindObjectOfType<AudioManager>().play("ButtonClick");
    }
}
