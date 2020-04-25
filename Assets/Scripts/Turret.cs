using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

/// <summary>
/// Represents a turret, and holds all attributes and logic related to the turret object
/// </summary>
public class Turret : MonoBehaviour
{
	private Transform target;

	[Header("Attributes")]
	
	public int initRange;
	public float intiFireRate = 1f;
	public float turnSpeed = 10f;
	public int price;
	public Sprite icon;
	public string shootShound;
	
	[Header("Unity Setup Fields")]
	
	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public GameObject bulletPrefab;
	public Transform firePoint;
	public GameObject RangeVisulaization;
	public GameObject coloredParent;
	public Material level2Material;
	public Material level3Material;

	//Object state
	private bool isSelected = false;
	private float fireCountdown = 0f;
	private int sellPrice;
	private int upgradePrice;
	private float fireRate;
	private int range;
	private int level = 1;
	private int maxLevel = 3;

	void Start ()
	{
		//initialize values
		sellPrice = price / 2;
		upgradePrice = (price / 2) + 10;
		fireRate = intiFireRate;
		range = initRange;
		
		updateRangeVisulization();
		RangeVisulaization.SetActive(false);
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	/// <summary>
	/// Updates the target, that is the enemy the turret is currently aiming at
	/// </summary>
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		} else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
			return;

		//Target lock on
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

		if(fireCountdown <= 0f){
			Shoot();
			fireCountdown = 1f/fireRate;
		}

		fireCountdown -= Time.deltaTime;
	}

	/// <summary>
	/// Makes the turret shoot, that is playing a sound, and creating a bullet
	/// </summary>
	void Shoot(){
		FindObjectOfType<AudioManager>().playOneShot(shootShound);
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();
		if(bullet != null){
			bullet.Seek(target);
		}
	}

	/// <summary>
	/// Handle selecting a placed turret, this either show or hide the range
	/// </summary>
	void OnMouseDown()
	{
		
		isSelected = !isSelected;
		RangeVisulaization.SetActive(isSelected);
		Selectable.instance.setTarget(this);
		Selectable.instance.activateStatePanel(isSelected);
	}
	
	/// <summary>
	/// Draws a sphere to represent the range
	/// </summary>
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	/// <summary>
	/// Upgrades the turret to the next leve,that is increasing the range, fire rate, sell price and upgrade price
	/// </summary>
	public void upgrade()
	{
		if (level + 1 <= maxLevel && Player.money >= upgradePrice)
		{
			Player.money -= upgradePrice;
			upgradeLook();
			this.level++;
			this.upgradePrice += price / 3;
			this.sellPrice += price / 4;
			this.range += 5;
			this.fireRate += 0.5f;
			this.updateRangeVisulization();	
		}
		else
		{
			string feedbackMessage = "";
			if (level + 1 > maxLevel)
			{
				feedbackMessage = "Already max level!";
			}
			else if(Player.money < upgradePrice)
			{
				feedbackMessage = "Not enough money!";
			}
			StartCoroutine(FeedbackHandler.instance.displayFeedbackText(feedbackMessage, 3,  Color.red));	
		}
	}

	/// <summary>
	/// Upgrades the look of the turret, that is changing the color of the turret according to turret level
	/// </summary>
	private void upgradeLook()
	{
		Material nextMaterial = null;
		if (level == 1)
		{
			nextMaterial = this.level2Material;
		} else if (level == 2)
		{
			nextMaterial = this.level3Material;
		}

		if (nextMaterial != null)
		{
			for (int i = 0; i < coloredParent.transform.childCount; i++)
			{
				coloredParent.transform.GetChild(i).GetComponent<MeshRenderer>().material = nextMaterial;
			}	
		}
		//transform.localScale += new Vector3(0.1f,0.1f,0.1f);
	}

	/// <summary>
	/// Updates the range vis size, that is the circle around the turret
	/// </summary>
	private void updateRangeVisulization()
	{
		RangeVisulaization.transform.localScale = new Vector3(range * 2, range * 2, range * 2);
	}

	/// <summary>
	/// Removes turret from game
	/// </summary>
	public void removeTurret()
	{
		Destroy(gameObject);
	}
	
	
	/**Getter & setters**/

	public int getSellPrice()
	{
		return this.sellPrice;
	}
	public int getUpgradePrice()
	{
		return this.upgradePrice;
	}
	public float getFireRate()
	{
		return this.fireRate;
	}
	public int getRange()
	{
		return this.range;
	}
	public int getLevel()
	{
		return this.level;
	}

	public int getMaxLevel()
	{
		return this.maxLevel;
	}
}