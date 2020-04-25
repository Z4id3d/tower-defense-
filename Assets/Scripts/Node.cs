using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a node (a place where a turret/weapon can be placed)
/// </summary>
public class Node : MonoBehaviour {

	public static List<Node> goList = new List<Node>(); //Contains all node objects that are in the map
	
	public Color hoverColor;
	public Color highlightColor;
	public Vector3 positionOffset;

	private GameObject turret;

	private Renderer rend;
	private Color startColor;

	private bool isVisible = false;
	private bool showingAllNodes = false;

	void Start ()
	{
		goList.Add(this);
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
    }

	/// <summary>
	/// Handles mouse click (mouse down), that includes checking if a turret is selected to be placed, checking if a
	/// node is taken, checking if there is enough money and so on
	/// </summary>
	void OnMouseDown ()
	{
		GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
		if (turret != null && turretToBuild != null)
		{
			StartCoroutine(FeedbackHandler.instance.displayFeedbackText("Can't build there!", 3,  Color.red));
			return;
		}
		
		if (turretToBuild != null)
		{
			Turret selectedTurret = turretToBuild.GetComponent<Turret>();
			if (Player.money >= selectedTurret.price)
			{
				turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
				hideAll();
				Player.money -= selectedTurret.price;
				BuildManager.instance.setTurretToBuild(null);
				StartCoroutine(FeedbackHandler.instance.displayFeedbackText("-$" + selectedTurret.price, 3,  Color.red));	
			}
			else
			{
				hideAll();
				StartCoroutine(FeedbackHandler.instance.displayFeedbackText("Not enough money to buy this turret!", 3,  Color.red));
				BuildManager.instance.setTurretToBuild(null);
			}
		} 
	}

	/// <summary>
	/// Handles the action when the mouse hovers over the node, it includes changes the color of the node to hover color
	/// and changing the state of node
	/// </summary>
	void OnMouseEnter ()
	{
		if (BuildManager.instance.GetTurretToBuild() != null)
		{
			if (isVisible)
			{
				hideNode();
			}
	
			rend.material.color = hoverColor;
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
			isVisible = true;
		}
	}

	/// <summary>
	/// Handles the action when the mouse exists out of the node, it includes changes the color of the node to normal color
	/// and changing the state of node
	/// </summary>
	void OnMouseExit ()
	{
		if (!showingAllNodes)
		{
			rend.material.color = startColor;
			if (isVisible)
			{
				hideNode();
			}	
		}
		else
		{
			rend.material.color = highlightColor;
		}
	}
	
	
	/// <summary>
	/// Makes the node visible to the player
	/// </summary>
	public void showNode()
	{
		if (!showingAllNodes)
		{
			showingAllNodes = true;
			rend.material.color = highlightColor;
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
			isVisible = true;	
		}
	}

	/// <summary>
	/// Makes the node invisible to the player
	/// </summary>
	private void hideNode()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
		isVisible = false;
	}


	/// <summary>
	/// Makes all nodes invisible to the player
	/// </summary>
	private void hideAll()
	{
		foreach(Node go in goList)
		{
			if (go.isVisible)
			{
				go.hideNode();
			}
			go.showingAllNodes = false;
			go.rend.material.color = startColor;
		}
	}
}