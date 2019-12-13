using UnityEngine;

namespace DefaultNamespace
{
	public class CollisionTester : MonoBehaviour
	{
	#region Methods

		void OnEnable()
		{
			transform.GetChild(0).gameObject.AddComponent<BoxCollider>();
			rigid            = gameObject.AddComponent<Rigidbody>();
			rigid.useGravity = false;
			CollisionManager = FindObjectOfType<CollisionManager>();
			Destroy(this,TimeToDestroy);
		}


		void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.GetComponent<CollisionTester>())
			{
				CollisionManager.Add(other);
				var entity                           = GetComponent<Entity>();
				if (entity != null) entity.moveSpeed = 0;
				transform.GetChild(0).GetComponent<Renderer>().material.color = Color.black;
				rigid.isKinematic                                             = true;
			}
		}

	#endregion

	#region Data

	#region Options

	#endregion

	#region States

		Rigidbody rigid	;

		CollisionManager CollisionManager  ;
		[SerializeField] float TimeToDestroy=.25f;

	#endregion

	#region Properties

	#endregion

	#endregion
	}
}
