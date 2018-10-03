using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Actions/Move/Pursuit")]
public class SoldierMovePursuit : SoldierMove {

    public override void DoAction(SoldierBrain agentBrain) {
        if (agentBrain.enemyTarget != null) {
            agentBrain.gameObject.GetComponent<NavMeshAgent>().destination = agentBrain.enemyTarget.transform.position;
        }
    }
}
