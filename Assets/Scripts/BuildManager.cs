using UnityEngine;

/// <summary>
/// Responsible for multiple things, mostly related to turrets and nodes
/// </summary>
public class BuildManager : MonoBehaviour {

	//Singleton 
	public static BuildManager instance;

	void Awake ()
	{
		if (instance != null)
		{
			return;
		}
		instance = this;
	}

	//Different type of turret prefabs that are possible to build
	public GameObject RTurretPrefab;
	public GameObject DoubleTurretPrefab;
	public GameObject FireTurretPrefab;
	public GameObject RucketTurretPrefab;
	public GameObject ShockTurretPrefab;
	private GameObject turretToBuild;
	
	/// <summary>
	/// Responsible for setting a turret to build
	/// </summary>
	/// <param name="turret">The turret to build, null is acceptable, and means no turret to build</param>
	public void setTurretToBuild (GameObject turret)
	{
		turretToBuild = turret;
		if (turret != null)
		{
			showAllNodes();	
		}
	}

	/// <summary>
	/// Returns the choosen turret to build
	/// </summary>
	public GameObject GetTurretToBuild ()
	{
		return turretToBuild;
	}
	
	/// <summary>
	/// Shows all positions where an item/turret can be placed
	/// </summary>
	private void showAllNodes()
	{
		foreach(Node go in Node.goList)
		{
			go.showNode();
		}
	}

}