using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private NavMeshAgent nav;
	Transform player;
	private FirstPersonController playerController;
	public float health;
	public Texture2D texture;
	public GameController gamecontroller;

	public float starthealth;
	//for sound
	public AudioSource[] sounds;
	public AudioSource noise1;
	public AudioSource noise2;
	// Use this for initialization
	void Start()
	{
		GameObject game = GameObject.FindGameObjectWithTag ("GameController");
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		gamecontroller = game.GetComponent<GameController> ();
		nav = this.GetComponent<NavMeshAgent>();
		if(gameObject.tag == "Enemy")
			health = 100.0f;
		else if(gameObject.tag == "Boss")
			health = 1000.0f;
		starthealth = health;
		gameObject.tag = "Enemy";
		gameObject.name = "Enemy";
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player != null) 
			playerController = player.GetComponent<FirstPersonController>();
		if (playerController == null) 
			Debug.Log ("Cannot Find 'FirstPersonController' script");
	}
	
	// Update is called once per frame
	void Update () {
	
		nav.SetDestination(player.position);
	}

	// Enemy attacking player
	void OnCollisionEnter(Collision collision) {
		ContactPoint contact = collision.contacts[0];

		if(contact.otherCollider.name=="Player")
		{

			if(gameObject.tag == "Enemy")
				playerController.PlayerDamage(10.0f);
			else if(gameObject.tag == "Boss")
				playerController.PlayerDamage(50.0f);
	
			if(playerController.GetHealth() <= 0.0f)
			{
				Application.LoadLevel ("gameover");
			}
		}
		else if(contact.otherCollider.tag=="Fireball")
		{
			//audio effects (fire Explosion)
			sounds = GetComponents<AudioSource>();
			noise1 = sounds[0];
			noise1.Play();
			gameObject.renderer.material.mainTexture = texture;
			//end fire explosion
			health -= 20.0f;
			if(health <= 0.0f)
			{
				gamecontroller.enemies_remaining--;
				gamecontroller.totalscore += (int)(starthealth)/10 + (int)nav.speed/10;
				Destroy(gameObject);
			}

		}
		else if(contact.otherCollider.tag=="Frostball")
		{

			//audio effects (fire Explosion)
			sounds = GetComponents<AudioSource>();
			noise1 = sounds[1];
			noise1.Play();
			//end fire explosion.
			gameObject.renderer.material.color = Color.blue;
			health -= 10.0f;
			if(health <= 0.0f)
			{
				gamecontroller.enemies_remaining--;
				gamecontroller.totalscore += (int)(health)/10 + (int)nav.speed/10;
				Destroy(gameObject);
			}
			if(gameObject.tag=="Enemy")
				nav.speed = 0.0f;
		}

	
	}
}	