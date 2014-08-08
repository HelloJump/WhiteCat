using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.StateMachines;
using NodeCanvas;
using whitecat.input;

public class WarriorBeh : AbsLifeUnitBeh 
{
    void AttackListener()
    {
        FSMState currentState = this.Fsm.GetStateWithName(this.Fsm.currentStateName);
        switch (this.Fsm.currentStateName)
        {
            case "Idle":
                if (!connectionCache.Contains("AttackFirstEvent"))
                    connectionCache.Push("AttackFirstEvent");
                break;
            case "AttackFirst":
                if (!connectionCache.Contains("AttackSecondEvent"))
                    connectionCache.Push("AttackSecondEvent");
                break;
            case "AttackSecond":
                if (!connectionCache.Contains("AttackThirdEvent"))
                    connectionCache.Push("AttackThirdEvent");
                break;
            case "AttackThird":
                if (!connectionCache.Contains("AttackLastEvent"))
                    connectionCache.Push("AttackLastEvent");
                break;
            default:
                break;
        }
    }

    void SkillStateListener()
    {
        this.Fsm.SendEvent("SkillStateEvent");
        //this.Bb.SetDataValue("", "");
    }

    void IdleListener()
    {
        this.Fsm.SendEvent("IdleEvent");
    }

    void MoveDestinationListener(Vector2 direction)
    {
        float x = this.cacheTransform.localPosition.x + direction.x;
        float y = this.cacheTransform.localPosition.y + direction.y;
        Vector3 bbValue = this.Bb.GetDataValue<Vector3>("CharacterDesPos");
        this.Bb.SetDataValue("CharacterDesPos", new Vector3(x, bbValue.y, y));
    }

    void MoveListener(whitecat.input.ScreenInputBeh.MoveState moveState)
    {
        switch (moveState)
        {
            case ScreenInputBeh.MoveState.ROLL:
                this.Bb.SetDataValue("IsRun", 3);
                break;
            case ScreenInputBeh.MoveState.RUN:
                this.Bb.SetDataValue("IsRun", 2);
                break;
            case ScreenInputBeh.MoveState.WALK:
                this.Bb.SetDataValue("IsRun", 1);
                break;
            default:
                break;
        }
        this.Fsm.SendEvent("MoveEvent");
    }

    void StagnantListener()
    {
        this.Fsm.SendEvent("SkillStagnantEvent");
    }

    void Update()
    {
        if (this.Bb.GetDataValue<bool>("StateDone"))
        {
            if(this.connectionCache.Count > 0)
                this.Fsm.SendEvent(this.connectionCache.Pop());
        }
    }

    protected override bool HasAttackTargetArround()
    {
        return base.HasAttackTargetArround();
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
