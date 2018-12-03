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
    public CellBehaviour cellHead;
    public Animator soldierAnim;
    bool isAttacking;
    // Use this for initialization
    void Start() {
        agentNav = gameObject.GetComponent<NavMeshAgent>();
        agentNav.speed = agentSpeed;
        destinationSet = false;
    }

	// Update is called once per frame
	void FixedUpdate () {
        soldierMove.DoAction(this);
	}
    void LateUpdate() {
        SetAnim();
    }

    public void StartDelay(float min, float max) {
        StartCoroutine(Delay(min,max));
    }

    public bool isDelaying;
    IEnumerator Delay(float min, float max) {
        if (!isDelaying) {
            isDelaying = true;
            soldierAnim.SetBool("isAttacking", false);
            yield return new WaitForSeconds(Random.Range(min, max));
                soldierMove.DelayHandler(this);
                isDelaying = false;
            destinationSet = false;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (enemyTGbrain != null)
        {
            if (coll.gameObject == enemyTGbrain.gameObject)
            {
                //soldierMove = null;
                //gameObject.GetComponent<NavMeshAgent>().destination = coll.gameObject.transform.position;
                StartCoroutine(AttackCD());
                isAttacking = true;
            }
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (enemyTarget != null)
        {
            if (coll.gameObject == enemyTGbrain.gameObject)
            {
                //soldierMove = null;
                StopCoroutine(AttackCD());
                isAttacking = false;
                soldierAnim.SetBool("isAttacking", false);
                soldierAnim.SetBool("isIdle", true);
            }
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
            enemyTGbrain.soldiers.Push(this);
        }
    }

    public void RemoveEnemy(bool cellRemoval = true) {
        if (shortEnemy != null)
        {
            enemyTarget = shortEnemy;
        }
        if(!cellRemoval)
        {
            cellHead.SetFree();
            enemyTarget = null;
            soldierMove.ChangeState(this, true);
        }
    }

    IEnumerator AttackCD() {
        soldierAnim.SetInteger("attacNum", 1);
        soldierAnim.SetBool("isIdle", false);
        soldierAnim.SetBool("isRunning", false);
        soldierAnim.SetBool("isAttacking", true);
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

    void SetAnim() {
        if (agentNav.velocity.magnitude > 2 && !isAttacking)
        {
            soldierAnim.SetInteger("runningNum", Random.Range(1, 3));
            soldierAnim.SetBool("isIdle", false);
            soldierAnim.SetBool("isAttacking", false);
            soldierAnim.SetBool("isRunning", true);
        }
        if(agentNav.velocity.magnitude < 2) {
            soldierAnim.SetInteger("idleNum", Random.Range(2, 6));
            soldierAnim.SetBool("isIdle", true);
            soldierAnim.SetBool("isRunning", false);
        }
    }
}
