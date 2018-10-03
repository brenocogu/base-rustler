using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Actions/Move/Wander")]
public class SoldierMoveWander : SoldierMove {

    public override void DoAction(SoldierBrain agentBrain) {
        if (!agentBrain.destinationSet)
        {
            agentBrain.destinationSet = true;
            agentBrain.gameObject.GetComponent<NavMeshAgent>().destination = RandomDestInNav(agentBrain);
        }
        
        else if(agentBrain.gameObject.GetComponent<NavMeshAgent>().remainingDistance < 1)
        { //TODO Arrumar delay quando transiona entre STANDBY => Wander
            if (!agentBrain.isDelaying){
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
