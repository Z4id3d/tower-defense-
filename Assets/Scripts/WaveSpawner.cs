using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Responsible for spawingin waves of enemies/zombies, handles rounds and difficulty, contains a set algorithms and logic
/// </summary>
public class WaveSpawner : MonoBehaviour{

	public GameObject[] enemies;
	public Transform spawnPoint;
	public Text enemyCountText;
	public Text roundNumberText;
	
	//Init round state values
	private int roundNumber = 0;
	private int enemyType = 1;
	public int enemyCount = 10;
	private int bossCount = 0;
	private float timeTillNextRound = 5.5f; //in seconds, wait 5 seconds before starting first round, then 5 seconds between rest of rounds
	//private float timeBetweenRounds = 20f; //in seconds, wait 20 seconds to start next round
	private float timeBetweenEnemySpawns = 0.7f;
	
	public static int enemiesAlive = 0;

	private bool playingSong = false;
	private int endOfRoundBonusAmount = 125;
	
	
	void Update ()
	{
		if (!playingSong && !FindObjectOfType<Tutorial>().isPlayingTutorial())
		{
			FindObjectOfType<AudioManager>().play("MainMusic");
			playingSong = true;
		}
		if (enemiesAlive == 0)//timeTillNextRound <= 0f) //TODO && prevround is finish (all enemies destroied)
		{
			if (timeTillNextRound <= 1f)
			{
				Player.money += endOfRoundBonusAmount;
				roundNumber++;
				StartCoroutine(SpawnWave());
				timeTillNextRound = 5.5f;
				roundNumberText.text ="Round " + roundNumber;
			}
			else
			{
				//roundNumberText.transform.position = new Vector3(Screen.width/2,Screen.height/2,0);
				//roundNumberText.text ="Next round in " + Mathf.Round(timeTillNextRound);
					
					StartCoroutine(StartOfRound.instance.shoMessage("Next round in " + Mathf.Round(timeTillNextRound), Mathf.Round(timeTillNextRound + 0.5f)));
					timeTillNextRound -= Time.deltaTime;	
				
			}
		}
		
		//waveCountdownText.text ="Next round: " + Mathf.Round(timeTillNextRound);
		enemyCountText.text ="Enemies alive: " + enemiesAlive;
		if (Player.lost)
		{
			FindObjectOfType<AudioManager>().stop("MainMusic");
		}
	}

	/// <summary>
	/// Spawns a wave of enemies, the number of enemies and the type depends on round number
	/// </summary>
	IEnumerator SpawnWave ()
	{
		for (int i = 0; i < enemyCount; i++)
		{
			int spawnPointIndex = getRandomNumberInRange(0, SpawnPoints.points.Length);
			spawnPoint = SpawnPoints.points[spawnPointIndex]; //Random spawnPoint for each enemy
			SpawnEnemy();
			yield return new WaitForSeconds(timeBetweenEnemySpawns);
		}

		enemyCount += 10; //Double enemies for next round
	}

	/// <summary>
	/// Spawns a single enemy, the enemy is random and the randomness depends on the round number
	/// </summary>
	void SpawnEnemy ()
	{
		int enemyIndex = 0;
		if (roundNumber - 1 >= 0 && roundNumber - 1 < enemies.Length)
		{
			enemyIndex = Random.Range(0, roundNumber);
		} else if(roundNumber - enemies.Length < enemies.Length)
		{
			enemyIndex = Random.Range(roundNumber - enemies.Length, enemies.Length);
		}
		else
		{
			enemyIndex = Random.Range(enemies.Length - 2, enemies.Length);
		}
		Instantiate(enemies[enemyIndex], spawnPoint.position, spawnPoint.rotation);
		enemiesAlive++;
	}
	
	/// <summary>
	/// Generates a random number in range. Returns a random number from a given lower to upper limit values
	/// </summary>
	private int getRandomNumberInRange(int lower, int upper)
	{
		return Random.Range(lower, upper);
	}

	/// <summary>
	/// Rests the number of enemies alive state.
	/// </summary>
	public static void resetEnemiesAlive()
	{
		enemiesAlive = 0;
	}
}
