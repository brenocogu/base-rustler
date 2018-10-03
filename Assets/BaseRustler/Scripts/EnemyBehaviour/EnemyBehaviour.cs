using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {
    NavMeshAgent nav;
    public GameObject castle;
    public float speed, hp;

    [HideInInspector]
    public List<CellBehaviour> cell;
	// Use this for initialization
	void Start () {
        castle = GameObject.FindGameObjectWithTag("Castle");
        nav = gameObject.GetComponent<NavMeshAgent>();
        nav.destination = castle.transform.position;
        nav.speed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeHit(float damage) {
        hp -= damage;
        if (hp <= 0) {
            gameObject.SetActive(false);
        }
    }

    void OnDisable() {
        if (cell != null) {
			foreach (CellBehaviour cel in cell) {
				cel.SendMessage ("RemoveEnemy", this.gameObject, SendMessageOptions.DontRequireReceiver);
			}
        }
    }
}
