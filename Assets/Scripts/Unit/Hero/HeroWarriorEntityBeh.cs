using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using whitecat.input;

public enum HeroWarriorAttackStage
{
    NONE,
    FIRSTATTACK,
    SECONDATTACK,
    THIRDATTACK,
    FORTHATTACK
}

public class HeroWarriorEntityBeh : AbsGameEntity
{
    public StateMachine<HeroWarriorEntityBeh> stateMachine;

    public override void Awake()
    {
        base.Awake();
        this.stateMachine = new StateMachine<HeroWarriorEntityBeh>(this);
        this.stateMachine.SetCurrentState(HeroWarriorIdleState.Instance());
    }

    public const int msgAttackFirst = 10001;

    public override void HandleMessage(Message message)
    {
        base.HandleMessage(message);
    }

    public override void Update()
    {
        this.stateMachine.UpdateState();
    }

    void MoveListener(MoveState moveState)
    {
        this.CurMoveState = moveState;
        switch (moveState)
        {
            case MoveState.ROLL:
                
                break;
            case MoveState.RUN:
                this.CurMoveState = MoveState.RUN;
                this.Speed = 2f;
                break;
            case MoveState.WALK:
                this.CurMoveState = MoveState.WALK;
                this.Speed = 1f;
                break;
            default:
                break;
        }
    }

    void MoveDestinationListener(Vector2 direction)
    {
        float x = this.CacheTransform.localPosition.x + direction.x;
        float y = this.CacheTransform.localPosition.y + direction.y;
        Vector3 lastDesPos = this.DesPos;
        this.DesPos = new Vector3(x, lastDesPos.y, y);
    }

    void IdleListener()
    {
        this.CurMoveState = MoveState.IDLE;
    }

    private HeroWarriorAttackStage curHeroWarriorAttackStage = HeroWarriorAttackStage.NONE;
    public HeroWarriorAttackStage CurHeroWarriorAttackStage
    {
        get { return this.curHeroWarriorAttackStage; }
        set { this.curHeroWarriorAttackStage = value; }
    }

    private int attackIdx = 0;
    public override void AttackListener()
    {
        this.CurHeroWarriorAttackStage = HeroWarriorAttackStage.FIRSTATTACK;

        //int attackIdx = (int)this.bb.GetDataValue<float>("AttackIdx");
        //int currentIdx = (int)this.bb.GetDataValue<float>("CurrentIdx");
        //switch (attackIdx)
        //{
        //    case 0:
        //        this.bb.SetDataValue("AttackIdx", currentIdx + 1f);
        //        break;
        //    case 1:
        //        this.bb.SetDataValue("AttackIdx", currentIdx + 1f);
        //        break;
        //    case 2:
        //        this.bb.SetDataValue("AttackIdx", currentIdx + 1f);
        //        break;
        //    case 3:
        //        this.bb.SetDataValue("AttackIdx", currentIdx + 1f);
        //        break;
        //    default:
        //        break;
        //}
    }

    public override bool HasValidTarget()
    {
        return base.HasValidTarget();
    }
    
    void SkillStateListener()
    {
 
    }

    void StagnantListener()
    {
 
    }

    public void MessageListener()
    {
        int attackIdx = (int)this.bb.GetDataValue<float>("AttackIdx");
        switch (attackIdx)
        {
            case 1:
                List<int> targetList = EntityManager.Instance().GetAttackTarget(GetInstanceID());
                for (int i = 0; i < targetList.Count; i++)
                {
                    Message message = new Message(msgAttackFirst, GetInstanceID(), targetList[i], null, 0.2f);
                    HandleMessage(message);
                }
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }
}
