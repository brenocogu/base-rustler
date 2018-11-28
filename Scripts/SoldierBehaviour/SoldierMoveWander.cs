using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Actions/Move/Wander")]
public class SoldierMoveWander : SoldierMove {

    public override void DoAction(SoldierBrain agentBrain) {
        agentBrain.soldierAnim.SetBool("isAttacking", false);
        if (!agentBrain.destinationSet)
        {
            agentBrain.destinationSet = true;
            agentBrain.gameObject.GetComponent<NavMeshAgent>().destination = RandomDestInNav(agentBrain);
            agentBrain.soldierAnim.SetInteger("runningNum", Random.Range(1, 3));
            agentBrain.soldierAnim.SetBool("isIdle", false);
            agentBrain.soldierAnim.SetBool("isRunning", true);
        }
        
        else if(agentBrain.gameObject.GetComponent<NavMeshAgent>().remainingDistance < 1)
        {
            if (!agentBrain.isDelaying){
                agentBrain.soldierAnim.SetInteger("idleNum", Random.Range(1, 3));
                agentBrain.soldierAnim.SetBool("isIdle", true);
                agentBrain.soldierAnim.SetBool("isRunning", false);
                agentBrain.StopAllCoroutines();
                agentBrain.StartDelay(2f, 5f);
            }
        }
    }

    Vector3 RandomDestInNav(SoldierBrain agentBrain) {
        return new Vector3(Random.Range(agentBrain.areamin.x, agentBrain.areamax.x), agentBrain.gameObject.transform.position.y, Random.Range(agentBrain.areamin.z, agentBrain.areamax.z));
    }
}

//100 SUAL
