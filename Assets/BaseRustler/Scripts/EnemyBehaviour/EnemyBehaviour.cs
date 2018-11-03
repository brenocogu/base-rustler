using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {
    GameManager gm;
    NavMeshAgent nav;
    public GameObject castle;
    public float speed, hp;

    [HideInInspector]
    public List<CellBehaviour> cell;
    [HideInInspector]
    public List<SoldierBrain> soldiers;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.IncreaseEnemyCount();
        castle = GameObject.FindGameObjectWithTag("Castle");
        nav = gameObject.GetComponent<NavMeshAgent>();
        nav.destination = castle.transform.position;
        nav.speed = speed;
	}

    public void TakeHit(float damage) {
        hp -= damage;
        if (hp == 0) {
            KillEnemy();
        }
    }

    public void KillEnemy() {
        if (hp <= 0) {
            gm.DecreaseEnemyCount();
            if (cell != null)
            {
                foreach (CellBehaviour cel in cell)
                {
                    cel.RemoveEnemy(this.gameObject);
                }
            }
            if (soldiers != null)
            {
                Debug.Log(soldiers.Count);
                foreach (SoldierBrain soldier in soldiers)
                {
                    soldier.RemoveEnemy(false);
                }
            }
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Castle") {
            coll.gameObject.GetComponent<CastleBehaviour>().HurtCastle();
            KillEnemy();
        }

        if (coll.gameObject.tag == "CellVision") {
            cell.Add(coll.gameObject.transform.parent.GetComponent<CellBehaviour>());
        }
    }
}
