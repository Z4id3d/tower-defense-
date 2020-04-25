using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contains actions to be performed on different main menu button clicks 
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Changes the scene from main menu to the game scene
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    /// <summary>
    /// Exits out of the game (works only outside of unity editor)
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
