using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellVision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Enemy") {
            SendMessageUpwards("AddEnemys", coll.gameObject, SendMessageOptions.DontRequireReceiver);           
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            SendMessageUpwards("RemoveEnemy", coll.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
}
