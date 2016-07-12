using UnityEngine;
using System.Collections;

//[RequireComponent (typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {
	
	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;
	public float jumpSpeed = 10.0f;
	
	float verticalRotation = 0;
	public float upDownRange = 60.0f;
	
	float verticalVelocity = 0;
	public GameObject fireshot;
	public GameObject frostshot;
	public GameObject poison;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	
	CharacterController characterController;
	//for sound
	public AudioSource[] sounds;
	public AudioSource noise1;//fire
	public AudioSource noise2;//frost
	public AudioSource noise3;//poison
	 
	// Health 
	private float playerhealth = 300.0f;
	
	// Use this for initialization
	void Start () {
		//Screen.lockCursor = true;
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation
		
		float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, rotLeftRight, 0);

		
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
		

		// Movement
		
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
		Physics.gravity = new Vector3(0, -15.0F, 0);
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		if( characterController.isGrounded && Input.GetButton("Jump") ) {
			verticalVelocity = jumpSpeed -10;
		
		}
	
		Vector3 speed = new Vector3( sideSpeed, verticalVelocity, forwardSpeed );
		
		speed = transform.rotation * speed;
		
		
		characterController.Move( speed * Time.deltaTime );
		//creating audio sources
		sounds = GetComponents<AudioSource>();
		noise1 = sounds[0];
		noise2 = sounds[1];
		noise3 = sounds[2];
		//end creating audio sources
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(fireshot, shotSpawn.position, shotSpawn.rotation);
			//add a sound here
			noise1.Play();


		}
		else if (Input.GetButton("Fire2") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(frostshot, shotSpawn.position, shotSpawn.rotation);
			//adding soundEffect
			noise2.Play();
		}
		else if(Input.GetButton("Fire3") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(poison, shotSpawn.position, shotSpawn.rotation);
			noise3.Play();
		}
			

	}

	public float GetHealth()
	{
		return playerhealth;
	}

	public void PlayerDamage(float damage)
	{
		playerhealth -= damage;
	}
}
