using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a shop and includes all logic for the shop
/// </summary>
public class Shop : MonoBehaviour
{
    private BuildManager buildManager;
    public Text[] priceTextElements;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    private void Update()
    {
        updateItemPriceColor();
    }

    /// <summary>
    /// Selects turret of the specified type and sets it in the build manager
    /// </summary>
    public void selectRTurret()
    {
        playButtonClickSound();
        buildManager.setTurretToBuild(buildManager.RTurretPrefab);
        Debug.Log("R turret selected");
    }
    
    /// <summary>
    /// Selects turret of the specified type and sets it in the build manager
    /// </summary>
    public void selectDoulbeTurret()
    {
        playButtonClickSound();
        buildManager.setTurretToBuild(buildManager.DoubleTurretPrefab);
        Debug.Log("Double turret selected");
    }
    
    /// <summary>
    /// Selects turret of the specified type and sets it in the build manager
    /// </summary>
    public void selectFireTurret()
    {
        playButtonClickSound();
        buildManager.setTurretToBuild(buildManager.FireTurretPrefab);
        Debug.Log("Fire turret selected");
    }
    
    /// <summary>
    /// Selects turret of the specified type and sets it in the build manager
    /// </summary>
    public void selectRucketTurret()
    {
        playButtonClickSound();
        buildManager.setTurretToBuild(buildManager.RucketTurretPrefab);
        Debug.Log("Rucket turret selected");
    }
    
    /// <summary>
    /// Selects turret of the specified type and sets it in the build manager
    /// </summary>
    public void selectShockTurret()
    {
        playButtonClickSound();
        buildManager.setTurretToBuild(buildManager.ShockTurretPrefab);
        Debug.Log("Shock turret selected");
    }
    
    /// <summary>
    /// Updates the color of the price text of the item in the shop to indicate weather the player have enough money
    /// for that turret or not
    /// </summary>
    private void updateItemPriceColor()
    {
        for (int i = 0; i < priceTextElements.Length; i++)
        {
            Text currentElement = priceTextElements[i];
            string cost = currentElement.text.Substring(1); //remove $ sign form text, and we are left with the money amount
            //Debug.Log(cost);
            if (int.Parse(cost) > Player.money)
            {
                currentElement.color = Color.red;
            }
            else
            {
                currentElement.color = Color.green;
            }
        }
    }
    
    /// <summary>
    /// Plays button click sound
    /// </summary>
    private void playButtonClickSound()
    {
        FindObjectOfType<AudioManager>().play("ButtonClick");
    }
}
