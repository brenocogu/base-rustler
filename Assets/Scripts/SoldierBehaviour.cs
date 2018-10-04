using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour {
    public float speed, sightRange, attackDamage;
    public GameObject _castle;
    List<GameObject> enemy;
    public LayerMask obstacles;
    float size;

    bool canWalk;
    // Use this for initialization
    void Start () {
        enemy = new List<GameObject>(20);
        canWalk = true;
        size = (gameObject.GetComponent<Renderer>().bounds.size.x + gameObject.GetComponent<Renderer>().bounds.size.z) / 2;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //Debug.Log(((4 - 0) ^ 2 + (5 - 0) ^ 2) < (4 ^ 2));
        if (enemy != null)
        {
            if (enemy.Count == 0)
            {
                Wander();
            }
            else
            {
                if (_currentTarget != null) {
                    transform.position = Vector3.MoveTowards(transform.position, _currentTarget.transform.position, speed * Time.deltaTime);
                    if (!_currentTarget.activeSelf) {
                        enemy.Remove(_currentTarget);
                        GetClosestEnemy();
                    }
                }
            }
        }
        else {
            Wander();
        }
	}

    void OnTriggerEnter(Collider coll) {
        if(coll.gameObject.tag == "Enemy")
        {
           enemy.Add(coll.gameObject);
           GetClosestEnemy();
        }
    }

    void OnTriggerExit(Collider coll) {
        if (coll.gameObject.tag == "Enemy")
        {
            enemy.RemoveAt(enemy.IndexOf(coll.gameObject));
            ResetPos();
            GetClosestEnemy();
        }
    }

    //Target Module
    GameObject _currentTarget;
    void GetClosestEnemy()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        enemy.ForEach(delegate (GameObject potentialTarget)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        });

        _currentTarget = bestTarget;

    }


    //Wander module
    bool IsWalkablePoint(Vector3 newPos) {       
        if (!Physics.CheckSphere(newPos, size + 0.1f, obstacles))
        {
            return true;    
        }
        return false;
    }

    void OnDrawGizmosSelected() {
        //Gizmos.DrawSphere(newPostion, size + 0.1f);
    }

    bool _newPosSet;
    Vector3 newPostion;
    Vector3 divergedPos;
    void Wander() {
        if (canWalk) {
            if (!_newPosSet)
            {
                float x = Random.Range(transform.position.x - 5.5f, transform.position.x + 5.5f);
                if (Mathf.Abs((new Vector3(x, 0, 0) - _castle.transform.position).x) < 10)
                {   
                    newPostion.x = x;
                    float z = Random.Range(transform.position.z - 5.5f, transform.position.z + 5.5f);
                    if (Mathf.Abs((new Vector3(0, 0, z) - _castle.transform.position).z) < 10)
                    { //Z Passed the test
                        newPostion.z = z;
                        
                        //if (Vector3.Distance(newPostion, castCenter.bounds.center) > castCenter.bounds.size.magnitude) {
                        if (IsWalkablePoint(newPostion)) {
                            _newPosSet = true;
                        }
                    }
                }
            }
            else {
                transform.position = Vector3.MoveTowards(gameObject.transform.position, newPostion, speed * Time.deltaTime);
                if(transform.position.x == newPostion.x && transform.position.z == newPostion.z)
                {
                    _newPosSet = false;
                    canWalk = false;
                    StartCoroutine(WalkWait());
                }
            }
        } 
    }

    void ResetPos() {
        _newPosSet = false;
        newPostion = Vector3.zero;
    }

    IEnumerator WalkWait() {
        yield return new WaitForSeconds(Random.Range(0.1f, 5f));
        canWalk = true;
    }
}
