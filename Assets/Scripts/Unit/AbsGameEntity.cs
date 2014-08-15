using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas;
using NodeCanvas.StateMachines;

public class AbsGameEntity : MonoBehaviour 
{
    public FSM fsm = null;
    public Blackboard bb = null;

    protected Stack<string> connectionCache = new Stack<string>();

    private Transform cacheTransform;
    public Transform GetGameEntityTrans()
    {
        return this.cacheTransform;
    }

    public virtual void HandleMessage(Message message)
    {

    }

    public virtual void Awake()
    {
        EntityManager.Instance().RegisterEntity(this);

        this.fsm = GetComponent<FSMOwner>().FSM;
        this.bb = GetComponent<Blackboard>();
    }

    public virtual void AttackListener()
    {

    }
}
