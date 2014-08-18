using UnityEngine;
using System.Collections;
using whitecat.input;

public class HeroWarriorIdleState : AbsState<HeroWarriorEntityBeh>
{
    private static HeroWarriorIdleState inst = null;
    public static HeroWarriorIdleState Instance()
    {
        if (inst == null)
            inst = new HeroWarriorIdleState();
        return inst;
    }

    public override void Enter(HeroWarriorEntityBeh gameEntity)
    {
        base.Enter(gameEntity);
    }

    public override void Execute(HeroWarriorEntityBeh gameEntity)
    {
        gameEntity.CacheAnimation.wrapMode = WrapMode.Loop;
        gameEntity.CacheAnimation.CrossFade("Idle", 0.3f, PlayMode.StopAll);
        if (gameEntity.CurMoveState != MoveState.IDLE)
        {
            gameEntity.stateMachine.ChangeState(HeroWarriorMoveState.Instance());
        }

        if (gameEntity.CurHeroWarriorAttackStage != HeroWarriorAttackStage.NONE)
        {
            gameEntity.stateMachine.ChangeState(HeroWarriorAttackState.Instance());
        }
        //base.Execute(gameEntity);
    }

    public override void Exit(HeroWarriorEntityBeh gameEntity)
    {
        base.Exit(gameEntity);
    }
}
