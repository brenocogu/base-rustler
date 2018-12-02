using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleBehaviour : MonoBehaviour {
    public GameObject goCanvas;
    public int hp;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.R)) {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
	}

    public void HurtCastle() {
        hp--;
        if (hp <= 0)
        {
            //Show G.O. Canvas
            Time.timeScale = 0;
            goCanvas.SetActive(true);
        }
    }
}
