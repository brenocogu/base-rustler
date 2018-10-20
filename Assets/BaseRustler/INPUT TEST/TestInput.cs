using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestInput : MonoBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 || Input.GetMouseButton(0))
        {
            Touch touch = Input.GetTouch(0);
            if ((touch.deltaTime < 0.2f && touch.phase == TouchPhase.Ended))
            { //Characterize an click in a mobile device OR for develop options, record the MouseButton0
                text.text = "TAP";
            }
            else if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                text.text = "DRAG";
            }
        }
    }
}
