using UnityEngine;

/// <summary>
/// Represents a bullet, which contains the bullet speed and damage. Also responsible for following a target and
/// "hitting" that target
/// </summary>
public class Bullet : MonoBehaviour {

	private Transform target; //Target of the bullet (the enemy that this bullet is shot at)

	public int damage = 50;
	public int speed = 70;
	public GameObject impactEffect;

	/// <summary>
	/// Sets the target to a given target
	/// </summary>
	/// <param name="_target">The target to set</param>
	public void Seek (Transform _target)
	{
		target = _target;
	}
	
	void Update () {

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}

	/// <summary>
	/// Hits the target, that is damage the target, and destroy the bullet object
	/// </summary>
	void HitTarget ()
	{
		//GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		//Destroy(effectIns, 2f);

		damageTarget(damage);
		//Destroy(target.gameObject);
		Destroy(gameObject);
	}


	/// <summary>
	/// Damages the target with the amount of damage the bullet does
	/// </summary>
	/// <param name="damage">The amount of damage to deal to the target</param>
	private void damageTarget(int damage)
	{
		Enemy enemy = target.GetComponent<Enemy>();
		enemy.takeDamage(damage);
	}

	/// <summary>
	/// Returns the amount of damage this bullet deals
	/// </summary>
	public int getDamage()
	{
		return this.damage;
	}

	/// <summary>
	/// Returns the speed of this bullet
	/// </summary>
	public int getSpeed()
	{
		return this.speed;
	}
}