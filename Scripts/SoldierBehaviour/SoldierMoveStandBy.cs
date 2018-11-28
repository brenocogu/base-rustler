using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Actions/Move/StandBy")]
public class SoldierMoveStandBy : SoldierMove
{
    public override void DoAction(SoldierBrain agentBrain)
    {
        agentBrain.soldierAnim.SetInteger("runningNum", Random.Range(1, 3));
        agentBrain.soldierAnim.SetBool("isIdle", false);
        agentBrain.soldierAnim.SetBool("isRunning", true);
        if (!agentBrain.destinationSet)
        {
            agentBrain.destinationSet = true;
            agentBrain.gameObject.GetComponent<NavMeshAgent>().destination = Vector3.zero;
        }
    }
}
