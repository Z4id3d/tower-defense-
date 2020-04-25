using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the safe house health, and responsible for performing other actions such as taking damage from the house
/// and setting the appropriate color for the health bar according to the current health 
/// </summary>
public class HouseHealth : MonoBehaviour
{
     public Image healthBar;
     public Text healthText;
     private float fullHouseHealth = 100f;
     private static float currentHouseHealth = 100f;

     void Update()
     {
         healthBar.fillAmount = (currentHouseHealth/fullHouseHealth);
         healthText.text =  "Safe house health: " + currentHouseHealth;
         changeColorAccordingTohealthProcentage();
         handleLose();
     }

     /// <summary>
     /// Changes the color of the health bar according to the current health 
     /// </summary>
     private void changeColorAccordingTohealthProcentage()
     {
         if (currentHouseHealth > 50)
         {
             healthBar.color = new Color32(0, 192, 68,100);
         }
         if (currentHouseHealth <= 50)
         {
             healthBar.color = new Color32(255, 89, 0,100);
         } 
         if (currentHouseHealth <= 30)
         {
             healthBar.color = new Color32(219, 33, 33,100);
         }
     }

     /// <summary>
     /// Reduces the house health by a given amount
     /// </summary>
     /// <param name="damage">The amount of damage to reduce the health with</param>
     public static void takeDamage(float damage)
     {
         currentHouseHealth -= damage;

         if (currentHouseHealth < 0)
         {
             currentHouseHealth = 0;
         }
     }

     /// <summary>
     /// Handles loosing situation, that is pauses the game, and shows end of game menu
     /// </summary>
     private void handleLose()
     {
         if (currentHouseHealth <= 0)
         {
             ApplicationActions.instance.pause();
             EndOfGameMenu.instance.showEndOfGameMenu();
             Player.lost = true;
         }
     }

     /// <summary>
     /// Resets health of the safe house to start health
     /// </summary>
     public static void resetHealth()
     {
         currentHouseHealth = 100;
     }
}
