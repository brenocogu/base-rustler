using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastleBehaviour : MonoBehaviour {
    bool isSelected;
    public Canvas menuCanvas, goCanvas;
    public int hp;
    bool died;
	public Text castleHP;
	// Use this for initialization
	void Start () {
        menuCanvas.gameObject.SetActive(false);
        died = false;
	}
	
	// Update is called once per frame
	void Update () {
		castleHP.text = ""+hp;
        if (isSelected) {
            menuCanvas.transform.LookAt(Camera.main.transform.position);


            if (Input.touchCount == 1 || Input.GetMouseButton(0))
            {
                RaycastHit hit = new RaycastHit();
                Ray clickRay = Camera.main.ScreenPointToRay((Input.touchCount > 0) ? Input.GetTouch(0).position : (Vector2)Input.mousePosition);
                if (Physics.Raycast(clickRay, out hit))
                {
                    if (hit.collider.gameObject.tag != "Castle")
                    {
                        Transform parent = hit.collider.gameObject.transform.parent;
                        if (parent != null)
                        {
                            if (parent.tag != "Castle")
                            {
                                ToggleMenu();
                                isSelected = false;
                            }
                        }
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.R)) {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
	}

    void ToggleMenu() {
        menuCanvas.gameObject.SetActive(!menuCanvas.gameObject.activeSelf);
    }

    public void SelectCastle() {
        if (!isSelected) {
            isSelected = true;
            ToggleMenu();
        }
    }

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Enemy") {
            coll.gameObject.SetActive(false);
            hp--;
            if (hp <= 0) {
                //Show G.O. Canvas
                Time.timeScale = 0;
                goCanvas.gameObject.SetActive(true);
            }
        }
    }
}
