using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for all selectable objects. Contains a set of actions to be performed on selectable objects such
/// as turrets
/// </summary>
public class Selectable : MonoBehaviour
{
    public Image icon;
    public Text turretLevel;
    public Text damage;
    public Text range;
    public Text fireRate;
    public Text turnSpeed;
    public Text bulletSpeed;
    public Text sellPrice;
    public Text upgradePrice;
    public Button upgradeButton;
    
    private Turret target;
    
    public static Selectable instance;
    
    void Awake ()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Selectable in scene!");
            return;
        }
        instance = this;
    }
    
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Display state panel the shows the state of the selected object
    /// </summary>
    public void activateStatePanel(bool isActive)
    {
        if (isActive && target != null)
        {
            updateStatsToDiplay();
        }
        this.gameObject.SetActive(isActive);
    }

    /// <summary>
    /// Sets the turret target to the to a new target 
    /// </summary>
    /// <param name="target">The target to be assigned to the class target</param>
    public void setTarget(Turret target)
    {
        this.target = target;
    }

    public void setTargetNode(Node targetNode)
    {
        
    }
    
    /// <summary>
    /// Updates the text state of the selected object
    /// </summary>
    private void updateStatsToDiplay()
    {
        this.icon.sprite = this.target.icon;
        this.turretLevel.text = this.target.getLevel() + "";
        this.damage.text = this.target.bulletPrefab.GetComponent<Bullet>().getDamage() + "";
        this.range.text = this.target.getRange() + "";
        this.fireRate.text = this.target.getFireRate() + "";
        this.turnSpeed.text = this.target.turnSpeed + "";
        this.upgradePrice.text = this.target.getUpgradePrice() + "$";
        this.sellPrice.text = this.target.getSellPrice() + "$";
        this.bulletSpeed.text = this.target.bulletPrefab.GetComponent<Bullet>().getSpeed() + "";
        if (target.getLevel() >= target.getMaxLevel())
        {
            this.upgradePrice.text = "MAX LEVEL!";   
        }
    }

    /// <summary>
    /// Handles upgrade button click, this includes upgrading the selected object
    /// </summary>
    public void handleUpgrade()
    {
        playButtonClickSound();
        target.upgrade();
        updateStatsToDiplay();
        if (target.getLevel() >= target.getMaxLevel())
        { 
            this.upgradePrice.text = "MAX LEVEL!"; 
            //upgradeButton.interactable = false;
        }
    }

    /// <summary>
    /// Handles sell button click, this includes selling the selected object
    /// </summary>
    public void handleSell()
    {
        playButtonClickSound();
        Player.money += target.getSellPrice();
        target.removeTurret();
        this.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// plays button click sound
    /// </summary>
    private void playButtonClickSound()
    {
        FindObjectOfType<AudioManager>().play("ButtonClick");
    }
}
