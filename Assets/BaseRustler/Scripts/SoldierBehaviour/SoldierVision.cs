using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierVision : MonoBehaviour {
    //List<GameObject> enemys;
    SoldierBrain brain;

    void Start() {
        brain = gameObject.GetComponentInParent<SoldierBrain>();
    }

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Enemy") {
            coll.GetComponent<EnemyBehaviour>().soldiers.Add(this.transform.parent.GetComponent<SoldierBrain>());
            //enemys.Add(coll.gameObject);
            brain.shortEnemy = coll.gameObject;
        }
    }

    void OnTriggerExit(Collider coll) {
        if (coll.gameObject.tag == "Enemy") {
            coll.GetComponent<EnemyBehaviour>().soldiers.Remove(this.transform.parent.GetComponent<SoldierBrain>());
            //enemys.Remove(coll.gameObject);
        }
    }
}
