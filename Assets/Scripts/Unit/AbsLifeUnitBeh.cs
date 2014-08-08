using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.StateMachines;
using NodeCanvas;

abstract public class AbsLifeUnitBeh : AbsUnitBeh
{
    private FSM fsm = null;
    public FSM Fsm
    {
        get
        {
            if (this.fsm == null)
                this.fsm = GetComponent<FSMOwner>().FSM;
            return this.fsm;
        }
    }

    private Blackboard bb = null;
    public Blackboard Bb
    {
        get
        {
            if (this.bb == null)
                this.bb = GetComponent<Blackboard>();
            return this.bb;
        }
    }

    protected Stack<string> connectionCache = new Stack<string>();

    virtual protected bool HasAttackTargetArround()
    {
        return false;
    }

    public float speed = 1;
    public float retainDistance = 1f;
}
