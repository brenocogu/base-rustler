using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SoldierMove : ScriptableObject {
    public float speed;
    public Vector3 destination;
    public SoldierMove trueState, falseState;

    public virtual void SetupScript<P>(P param) { }
    public virtual void DoAction(SoldierBrain agentBrain) { }
    public virtual void DelayHandler(SoldierBrain agentBrain) { }
    public virtual void ChangeState(SoldierBrain agentBrain, bool stateCond) { agentBrain.soldierMove = (stateCond) ? trueState : falseState; }
}
