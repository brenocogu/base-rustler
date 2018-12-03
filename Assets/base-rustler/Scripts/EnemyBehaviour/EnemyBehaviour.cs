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
    public Stack<SoldierBrain> soldiers;
	// Use this for initialization
	void OnEnable () {
        hp = 4;
        soldiers = new Stack<SoldierBrain>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.IncreaseEnemyCount();
        Reset();
	}
    void Reset() {
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
    void OnDisable() {
        gm.DecreaseEnemyCount();
    }
    public void KillEnemy() {
        if (hp <= 0) {
            if (cell != null)
            {
                foreach (CellBehaviour cel in cell)
                {
                    cel.RemoveEnemy(this.gameObject);
                }
            }
            if (soldiers != null)
            {
                for (int i = 0; i <= soldiers.Count; i++) {
                    if (soldiers.Count > 0)
                    {
                        soldiers.Pop().RemoveEnemy(false);
                    }
                }
            }
            hp = 4;
            if (soldiers.Count == 0)
            {
                gameObject.SetActive(false);
            }
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

    void Update() {
        if (Input.GetKeyUp(KeyCode.L))
        {
            castle = GameObject.FindGameObjectWithTag("Castle");
            nav = gameObject.GetComponent<NavMeshAgent>();
            nav.destination = castle.transform.position;
            nav.speed = speed;
        }
    }
}
