using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public GameObject earth;
	public float strengthOfAttraction = 5.0f;
	public float moveSpeed = 20f;
	public float jumpPower = 30f;
	float startTime;
	Rigidbody2D rigidbody2D;

	public Text scoreLabel;

	public bool isOnGround = false;
	public bool dead = false;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		startTime = Time.time;
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


		// look at earth
		Vector2 dir = earth.transform.position - transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	

		// jump
		if (Input.GetKeyDown ("space") && isOnGround){

			Vector2 diff = earth.transform.position - transform.position;
			rigidbody2D.AddForce(diff * jumpPower * -1, ForceMode2D.Impulse);

			isOnGround = false;
		}

		if (Input.GetKey("a")) {
			transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
		}

		if (Input.GetKey("d")) {
			transform.Translate(-Vector3.left * moveSpeed * Time.deltaTime);
		}

		scoreLabel.text = "Score: " + (int)(Time.time - startTime);

	}
	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject == earth) {
			isOnGround = true;
		}

		if (coll.gameObject.tag == "Rock") {
			Die ();
		}
	}

	void Die() {
		Destroy(gameObject);
		dead = true;

	}
	
}
