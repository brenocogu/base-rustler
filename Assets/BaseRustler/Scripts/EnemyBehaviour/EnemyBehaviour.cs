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
        if (hp <= 0) {
            KillEnemy();
        }
    }

    void KillEnemy() {
        gm.DecreaseEnemyCount();
        if (cell != null)
        {
            foreach (CellBehaviour cel in cell)
            {
                cel.SendMessage("RemoveEnemy", this.gameObject, SendMessageOptions.DontRequireReceiver);
            }
        }
        gameObject.SetActive(false);
    }
}
