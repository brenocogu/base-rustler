using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Actions/Move/Pursuit")]
public class SoldierMovePursuit : SoldierMove {

    public override void DoAction(SoldierBrain agentBrain) {
        if (agentBrain.enemyTarget != null)
        {
            agentBrain.soldierAnim.SetInteger("attacNum",1);
            agentBrain.soldierAnim.SetBool("isIdle", false);
            agentBrain.soldierAnim.SetBool("isRunning", false);
            agentBrain.soldierAnim.SetBool("isAttacking", true);
            if (agentBrain.enemyTarget.activeSelf)
            {
                agentBrain.gameObject.GetComponent<NavMeshAgent>().destination = agentBrain.enemyTarget.transform.position;
            }
            else
            {
                agentBrain.RemoveEnemy(false);
            }

        }
        else {
            agentBrain.RemoveEnemy(false);
        }
    }
}
