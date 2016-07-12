using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public int speed;
	public GameObject explosion;


	void Update () {
	
		transform.position += transform.forward * Time.deltaTime * speed;
		
	}

	void OnCollisionEnter(Collision collision) {

		Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
