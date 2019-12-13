using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
	public List<Collision> Collisions = new List<Collision>();
	[SerializeField] float RadiusMultiplier=20f;



	public void Add(Collision other)
	{
		Collisions.Add(other);
		Debug.Log("Collision happened!");

	}

	public void OnDrawGizmos()
	{
		foreach (var collision in Collisions)
		{

			Gizmos.DrawWireSphere(collision.GetContact(0).point,RadiusMultiplier);

		}
	}
}
