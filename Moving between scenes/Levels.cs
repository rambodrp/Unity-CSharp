using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour {

	public void RESTARTDALEVEL(){
		string sceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
	}

	public void mainMenu(){
		string sceneName = "Main";
		SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
	}

	// Use this for initialization
	void Start () {
		Debug.Log("Started");
	}
	// Update is called once per frame
	// void Update () {

	// }
}