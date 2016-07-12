using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject enemy;
	public GameObject boss;
	public GameObject player;
	private int enemycount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int totalenemies;
	public FirstPersonController playercontroller;
	public int enemies_remaining;
	public int totalscore;


	public GUIText remenemies;
	public GUIText health;
	public GUIText score;
	private bool bosspawned;

	// Use this for initialization
	void Start () {
		enemycount = 0;
		totalscore = 0;
		playercontroller = player.GetComponent<FirstPersonController> ();
		enemies_remaining = totalenemies;
		bosspawned = false;
		StartCoroutine(SpawnEnemies ());
	}

	IEnumerator SpawnEnemies()
	{
		while(enemycount <= totalenemies )
		{
			float x1 = player.transform.position.x +20.0f;
			float x2 = player.transform.position.x -20.0f;
			float z1 = player.transform.position.z +20.0f;
			float z2 = player.transform.position.z -20.0f;
			Vector3 spawnposition = new Vector3 (Random.Range (x1,x2), 0.0f, Random.Range (z1,z2));
				
			Instantiate (enemy, spawnposition, Quaternion.identity);
			enemycount++;

			if(/*enemies_remaining == (totalenemies-5) &&*/ bosspawned == false)
			{
				Vector3 bossspawnposition = new Vector3 (6.0f, -1.0f, 590.0f);
				
				Instantiate (boss, bossspawnposition, Quaternion.identity);
				bosspawned = true;
				//enemies_remaining++;
			}

			yield return new WaitForSeconds(spawnWait);

		}

	}

	void OnGUI()
	{
		remenemies.text = "Enemies Remaining: " + enemies_remaining;
		health.text = "Health: " + playercontroller.GetHealth();
		score.text = "SCORE: " + totalscore;

		if(enemies_remaining == 0)
			Application.LoadLevel ("winscreen");
	}


}
