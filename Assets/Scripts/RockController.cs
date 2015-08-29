using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RockController : MonoBehaviour {

	public GameObject earth;
	public float strengthOfAttraction = 5.0f;
	Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		earth = GameObject.Find("Earth");
	}
	
	// Update is called once per frame
	void Update () {
		// go to earth
		Vector3 offset = earth.transform.position - transform.position;
		offset.z = 0;
		
		//get the squared distance between the objects
		float magsqr = offset.sqrMagnitude;
		
		//check distance is more than 0 (to avoid division by 0) and then apply a gravitational force to the object
		//note the force is applied as an acceleration, as acceleration created by gravity is independent of the mass of the object
		if(magsqr > 0.0001f) {
			rigidbody2D.AddForce(strengthOfAttraction * offset.normalized / magsqr, ForceMode2D.Force);
		}
	
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.name == "Earth") {
			Destroy(gameObject);
		}
	}
}
