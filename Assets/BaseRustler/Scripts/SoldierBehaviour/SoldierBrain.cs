using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SoldierBrain : MonoBehaviour {
    public SoldierMove soldierMove;
    [HideInInspector]
    public bool assignedToarea, destinationSet;

    [HideInInspector]
    public GameObject enemyTarget, shortEnemy;
    EnemyBehaviour enemyTGbrain;

    public float attackSpeed, attackDamage;

    [HideInInspector]
    public Vector3 areamax, areamin;
    public float agentSpeed;
    NavMeshAgent agentNav;
    // Use this for initialization
    void Start() {
        agentNav = gameObject.GetComponent<NavMeshAgent>();
        agentNav.speed = agentSpeed;
        destinationSet = false;
    }

	// Update is called once per frame
	void Update () {
        soldierMove.DoAction(this);
	}

    public void StartDelay(float min, float max) {
        StartCoroutine(Delay(min,max));
    }

    public bool isDelaying;
    IEnumerator Delay(float min, float max) {
        if (!isDelaying) {
            isDelaying = true;
            yield return new WaitForSeconds(Random.Range(min, max));
                soldierMove.DelayHandler(this);
                isDelaying = false;
            destinationSet = false;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy") {
            //soldierMove = null;
            //gameObject.GetComponent<NavMeshAgent>().destination = coll.gameObject.transform.position;
            StartCoroutine(AttackCD());
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            //soldierMove = null;
            StopCoroutine(AttackCD());
        }
    }

    public void AssignArea(Vector3 max, Vector3 min){
        assignedToarea = true;
        areamax = max;
        areamin = min;
        gameObject.GetComponent<NavMeshAgent>().destination = gameObject.transform.position;
        soldierMove.ChangeState(this, true);
        StopCoroutine(Delay(1f, 1f));
        destinationSet = false;
    }

    public void GetEnemy(GameObject enemy, bool shortEnemyTargeting = false) {
        if (!shortEnemyTargeting)
        {
            enemyTarget = enemy;
            soldierMove.ChangeState(this, true);
            enemyTGbrain = enemyTarget.GetComponent<EnemyBehaviour>();
        }
    }

    public void RemoveEnemy(bool cellRemoval = true) {
        if (shortEnemy != null)
        {
            enemyTarget = shortEnemy;
        }
        if(!cellRemoval)
        {
            
            enemyTarget = null;
            soldierMove.ChangeState(this, true);
        }
    }

    IEnumerator AttackCD() {
        yield return new WaitForSeconds(attackSpeed);
        enemyTGbrain.TakeHit(attackDamage);
        StartCoroutine(AttackCD());
    }

    public void UnassignSoldier() {
        gameObject.GetComponent<NavMeshAgent>().destination = gameObject.transform.position;
        soldierMove.ChangeState(this, false);
        assignedToarea = false;
        destinationSet = false;
    }
}
