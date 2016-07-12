using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public int speed;
	public GameObject explosion;
	void Update () {
	
		transform.position += transform.forward * Time.deltaTime * speed;
	}

	void OnCollisionEnter(Collision collision) {
		ContactPoint contact = collision.contacts[0];
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		Vector3 pos = contact.point;
		Instantiate(explosion, pos, rot);
		Destroy(gameObject);
	}
}
