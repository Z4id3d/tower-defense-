using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for displaying feedback messages
/// </summary>
public class FeedbackHandler : MonoBehaviour{
    
    public static FeedbackHandler instance;
    void Awake ()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public Text feedbackMessageText;

    /// <summary>
    /// Display a messages for a specified duration
    /// </summary>
    /// <param name="message">Message to be displayed</param>
    /// <param name="delay">Duration of message</param>
    /// <param name="color">Color of message</param>
    public IEnumerator displayFeedbackText (string message, float delay, Color color) {
        feedbackMessageText.fontSize = 30;
        feedbackMessageText.color = color;
        feedbackMessageText.transform.position = new Vector3(Screen.width/2, Screen.height/2 + 100);
        feedbackMessageText.text = message;
        feedbackMessageText.enabled = true;
        yield return new WaitForSeconds(delay);
        feedbackMessageText.enabled = false;
    }
}