using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds all spawn points as children, adds them to a public accessible list
/// </summary>
public class StartOfRound : MonoBehaviour
{
    public Text startOfRoundText;
    
    public static StartOfRound instance;

    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    //public void showStartOfRoundText(string text, float delay)
    //{
    //    StartCoroutine(shoMessage(text, delay));
    //}

    /// <summary>
    /// Displays start of round message telling the user when the next round is going to start
    /// </summary>
    public IEnumerator shoMessage(string text, float delay)
    {
        if (!FindObjectOfType<Tutorial>().isPlayingTutorial() && !Player.lost)
        {
            this.startOfRoundText.text = text;
            gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); 
    }
}
