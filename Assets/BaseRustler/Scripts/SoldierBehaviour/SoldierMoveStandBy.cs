using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Actions/Move/StandBy")]
public class SoldierMoveStandBy : SoldierMove
{
    public override void DoAction(SoldierBrain agentBrain)
    {
        if (!agentBrain.destinationSet)
        {
            agentBrain.destinationSet = true;
            agentBrain.gameObject.GetComponent<NavMeshAgent>().destination = Vector3.zero;
        }
    }
}
