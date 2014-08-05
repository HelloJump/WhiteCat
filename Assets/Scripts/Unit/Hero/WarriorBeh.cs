using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.StateMachines;
using NodeCanvas;

public class WarriorBeh : MonoBehaviour 
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

    private HeroAttack heroAttack = null;
    void AttackListener()
    {
        switch (this.heroAttack.attackIdx)
        {
            case 0:
                this.Fsm.SendEvent("AttackFirstEvent");
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    void Awake()
    {
        this.heroAttack = new HeroAttack(0, 1f, 1.433f);
    }
}
