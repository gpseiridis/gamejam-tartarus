using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {

	public GameObject enemy;
	public GameController gamecontroller;

	void OnParticleCollision(GameObject other) 
	{
		GameObject game = GameObject.FindGameObjectWithTag ("GameController");
		gamecontroller = game.GetComponent<GameController> ();

		EnemyController enemycontroller = enemy.GetComponent<EnemyController> ();
		Debug.Log (other.gameObject.tag);
		if(other.gameObject.tag == "Enemy")
		{
			enemycontroller.health -=10.0f;
			if(enemycontroller.health <=0.0f)
			{
				gamecontroller.enemies_remaining--;
				gamecontroller.totalscore += (int)enemy.GetComponent<EnemyController>().starthealth/10 + (int)enemy.GetComponent<NavMeshAgent>().speed/10; 
				Destroy (other.gameObject);
			}
		}
	}
}
