using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds the player state, that is the amount of money and if lost or not.
/// Also contains some actions to be performed
/// </summary>
public class Player : MonoBehaviour
{
    public static int money = 200;
    public static bool lost = false;

    public Text moneyText;
    void Start()
    {
        
    }

    void Update()
    {
        updateMoneyText(money);
    }

    /// <summary>
    /// Updates money text with the new money amount
    /// </summary>
    /// <param name="newAmount">Then new money amount</param>
    private void updateMoneyText(int newAmount)
    {
        moneyText.text = "$" + newAmount;
    }

    
    /// <summary>
    /// Resets the player money to the start money
    /// </summary>
    public static void restMoney()
    {
        money = 200;
    }
    
    /// <summary>
    /// Resets lost to the start value
    /// </summary>
    public static void restLostState()
    {
        lost = false;
    }
}
