using UnityEngine;
using System.Collections;

/// <summary>
/// Represents an enemy, holds all enemy attributes and actions that the enemy can perform
/// </summary>
public class Enemy : MonoBehaviour
{
	//Enemy attributes
	public int health;
	public float speed;
	public int worth;
	public float damage;
	private Transform target;
    private Transform[] wayPoints;
    private int wavepointIndex = 0;
    private int waypointTochoose = -1; //Set this to 1, 2 or 3
    private bool isAlive = true;

    void Start()
    {
	    assignWayPoints();
    }

    void Update()
    {
	    if (health <= 0)
	    {
		    die();
	    }

	    if (isAlive)
	    {
		    Move();   
	    }
    }
    
    /// <summary>
    /// Moves the enemy to the next waypoint
    /// </summary>
    private void Move(){
		rotateEnemy();
		updatePosition();
		if(reachedWayPoint()){
			goToNextWayPoint();
		}
    }

    /// <summary>
    /// Checks if the enemy reached the waypoint it is moving towards
    /// </summary>
	private bool reachedWayPoint(){
		bool reachedWayPoint = false;
		if(Vector3.Distance(target.position, transform.position) <= 0.2f){
			reachedWayPoint = true;
		}
		return reachedWayPoint;
	}

	/// <summary>
	/// Switches the target waypoint to the next waypoint, and destroies enemy object if all waypoints passed
	/// </summary>
	private void goToNextWayPoint(){
		wavepointIndex++;
		if(wavepointIndex < wayPoints.Length){
			target = wayPoints[wavepointIndex]; 		
		} else {
			WaveSpawner.enemiesAlive--; //Update number of enemies alive
			HouseHealth.takeDamage(this.damage);
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// Updates the position of the enemy (moves it to the target)
	/// </summary>
	private void updatePosition()
	{
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}
	
	/// <summary>
	/// Rotates the enemy so that it always is looking at the target waypoint
	/// </summary>
	private void rotateEnemy(){
		var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
 		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
	}

	/// <summary>
	/// Finds the waypoint that is closest to the spawn-point, and assignes it to that enemy
	/// </summary>
	private void assignWayPoints()
	{
		int spawnPoint = -1;
		Vector3 enemyPosition = transform.position;
		Transform[] spawnPoints = SpawnPoints.points;
		int i = 0;
		while(spawnPoint == -1 && i < spawnPoints.Length)
		{
			Vector3 currentSpawnPointPosition = spawnPoints[i].position;
			if (enemyPosition.Equals(currentSpawnPointPosition))
			{
				spawnPoint = i;
			}

			i++;
		}
		wayPoints = WayPointGroups.wayPointArrays[spawnPoint];

		target = wayPoints[0];
	}


	/// <summary>
	/// Reduces the enemy health with the given amount
	/// </summary>
	/// <param name="amount">The amount of damage to reduce the health with</param>
	public void takeDamage(int amount)
	{
		health -= amount;
		if (health > 0)
		{
			playTakingDamageAnimation();
		}
	}
	
	/// <summary>
	/// Kills the enemy, that includes removing the tag, playing death animation, increases player money,
	/// reduces enemies alive and destroy the enemy object
	/// </summary>
	private void die()
	{
		//Remove enemy tag
		setTag("Untagged");
		
		if (isAlive)
		{
			//Update enemies alive
			WaveSpawner.enemiesAlive--;
			
			//Give player money for the kill
			Player.money += worth;
		}

		setDead();
		playRandomDeathAnimation();

		//Remove dead body/object from game
		Destroy(gameObject, 2f);
	}
	
	/// <summary>
	/// Updates alive state of enemy to dead
	/// </summary>
	private void setDead()
	{
		isAlive = false;
	}

	/// <summary>
	/// Chooses a random death animation of the death animations we have and play it
	/// </summary>
	private void playRandomDeathAnimation() //TODO return animation duration, get it from animator
	{
		int randNum = Random.Range(1, 3); //Get random number from, either 1 or 2
		Animator animator = GetComponent<Animator>();
		//Debug.Log(Mathf.Round(randNum));
		if (Mathf.Round(randNum) == 1)
		{
			animator.SetTrigger("isDead");
		} else
		{
			animator.SetTrigger("isDeadt");
		}
	}

	/// <summary>
	/// Play animation of being hit
	/// </summary>
	private void playTakingDamageAnimation()
	{
		Animator animator = GetComponent<Animator>();
		animator.SetTrigger("isTakingDamage");	
	}
	
	/// <summary>
	/// Removes the current tag and updates it with a new tag
	/// </summary>
	private void setTag(string newTag)
	{
		gameObject.tag = newTag;
	}
}
