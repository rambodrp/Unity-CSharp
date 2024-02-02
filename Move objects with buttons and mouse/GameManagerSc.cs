using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSc : MonoBehaviour {

	public GameObject Bullet, FlashMuzzle, player, zombieGirl, zombieBoy;
	public GameObject bulletInitGo;
	private Transform bulletInitTrans;
	private float nextActionTime = 0, period = 0.07f, playerSpeed = 5.0f;
	public Animator playerAnims, playerLegAnims, playerFireAnim;
	public AudioSource playerAudioSource;
	public AudioClip ak47Firing;
	public bool moveLeft = false;
	private int playerLife = 5;
	public int playerLifeLeft = 5;

	// Use this for initialization
	void Start () {
		playerLifeLeft = playerLife;
		bulletInitTrans = bulletInitGo.GetComponent<Transform> ();
		StartCoroutine (zombieInitOverTime());
		//playerAnims = GameObject.Find("PlayerHead").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Check mouse for changing Player position
		if (playerLifeLeft > 0) {
			Vector3 pos = Input.mousePosition;
			Vector3 posPlayer = player.transform.position;
			pos.z = 20;
			pos = Camera.main.ScreenToWorldPoint (pos);
			if (pos.x < posPlayer.x) {
				if (player.transform.localScale.x > 0) {
					moveLeft = true;
					player.transform.localScale = new Vector2 (-player.transform.localScale.x, player.transform.localScale.y);
				}

				//Debug.Log (pos.x + "<=|||=>" + posPlayer.x);
			} else {
				if (player.transform.localScale.x < 0) {
					moveLeft = false;
					player.transform.localScale = new Vector2 (-player.transform.localScale.x, player.transform.localScale.y);
				}
			}

			if (Input.GetMouseButtonDown (0)) {

				nextActionTime = period + Time.time;
				gunFire ();
			}
			if (Input.GetMouseButtonUp (0)) {
				playerAnims.SetBool ("ScaredFace", false);
				playerFireAnim.SetBool ("Fire", false);
			}

			if (Input.GetMouseButton (0)) {
			
				playerAnims.SetBool ("ScaredFace", true);
				playerFireAnim.SetBool ("Fire", true);
				if (Time.time > nextActionTime) {
					nextActionTime = period + Time.time;
					gunFire ();
				}
			}

			if (Input.GetMouseButtonDown (1)) {
				Debug.Log ("Pressed right click.");
			}

			if (Input.GetMouseButtonDown (2)) {
				Debug.Log ("Pressed middle click.");
			}

			if (Input.GetKey (KeyCode.A)) {
				playerLegAnims.SetBool ("Walk", true);
				movePlayer ("left");
			}
			if (Input.GetKey (KeyCode.D)) {
				playerLegAnims.SetBool ("Walk", true);
				movePlayer ("right");
			}
			if (Input.GetKey (KeyCode.Space) || Input.GetKey (KeyCode.W)) {
				movePlayer ("jump");
			}
			if (Input.GetKey (KeyCode.S)) {
				movePlayer ("down");
			}
			if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) {
				playerLegAnims.SetBool ("Walk", false);
			}

		}
	}



	private void gunFire(){
		playerAudioSource.PlayOneShot (ak47Firing,1.0f);
		GameObject newMuzzle =  Instantiate (FlashMuzzle, new Vector2(bulletInitTrans.position.x, bulletInitTrans.position.y),Quaternion.identity) as GameObject;
		if (moveLeft) {
			newMuzzle.transform.localScale = new Vector2 (-newMuzzle.transform.localScale.x, newMuzzle.transform.localScale.y);
			newMuzzle.GetComponent<Transform> ().position = new Vector2 (newMuzzle.GetComponent<Transform>().position.x-0.33f, newMuzzle.GetComponent<Transform>().position.y);
		} else {
			newMuzzle.transform.localScale = new Vector2 (newMuzzle.transform.localScale.x, newMuzzle.transform.localScale.y);
			newMuzzle.GetComponent<Transform> ().position = new Vector2 (newMuzzle.GetComponent<Transform>().position.x+0.33f, newMuzzle.GetComponent<Transform>().position.y);
		}
		newMuzzle.GetComponent<FlashMuzzleSc> ().startMuzzle ();
		newMuzzle.GetComponent<Transform> ().rotation = Quaternion.Euler(0,0,180);


	}
		
	private void movePlayer(string whichWay){
		if(whichWay == "left"){
			player.transform.position += player.transform.right * Time.deltaTime*-playerSpeed;
		}else if(whichWay == "right"){
			player.transform.position += player.transform.right * Time.deltaTime*playerSpeed;
		}else if(whichWay == "down"){
			Debug.Log ("Down");
			//player.transform.position += player.transform.right * Time.deltaTime*playerSpeed;
		}else if(whichWay == "jump"){
			Debug.Log ("Jump");
			//player.transform.position += player.transform.right * Time.deltaTime*playerSpeed;
		}
	}

	IEnumerator zombieInitOverTime(){
		while (true) {
			yield return new WaitForSeconds (0.5f);
			bool right = true;
			if (Random.Range (1, 3) > 1) {
				right = false;
			}
			zombieInit (right, 5.0f);
		}
	}

	private void zombieInit(bool rightInit, float speed){
		GameObject Zombie = zombieGirl;
		if (Random.Range (1, 3) > 1) {
			Zombie = zombieBoy;
		}
		Vector2 initPos = new Vector2(12.2f, 6.5f);
		if(!rightInit){
			initPos = new Vector2(-9.0f, 6.5f);
		}

		GameObject newZombie = Instantiate (Zombie, initPos, Quaternion.identity) as GameObject;
		newZombie.GetComponent<Zombiemanager> ().zombieSpeed = speed;
		if(rightInit){
			newZombie.GetComponent<Zombiemanager> ().zombieSpeed = -speed;
			newZombie.transform.localScale = new Vector2 (-newZombie.transform.localScale.x, newZombie.transform.localScale.y);
		}

	}

	public void playerGotHit(int hpLost){
		playerLife -= hpLost;
		playerLifeLeft = playerLife;
		Debug.Log ("Hp: " + playerLife);
		if(playerLife <= 0){
			Destroy (player);
		}
	}

}
