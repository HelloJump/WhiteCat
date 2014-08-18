using UnityEngine;
using System.Collections;

public class HeroWarriorAttackState : AbsState<HeroWarriorEntityBeh>
{
    private static HeroWarriorAttackState inst = null;
    public static HeroWarriorAttackState Instance()
    {
        if (inst == null)
            inst = new HeroWarriorAttackState();
        return inst;
    }

    public override void Enter(HeroWarriorEntityBeh gameEntity)
    {
        base.Enter(gameEntity);
    }

    public override void Execute(HeroWarriorEntityBeh gameEntity)
    {
        switch (gameEntity.CurHeroWarriorAttackStage)
        {
            case HeroWarriorAttackStage.NONE:
                break;
            case HeroWarriorAttackStage.FIRSTATTACK:
                gameEntity.CacheAnimation.wrapMode = WrapMode.Once;
                gameEntity.CacheAnimation.CrossFade("Attack", 0.3f, PlayMode.StopAll);
                gameEntity.CurHeroWarriorAttackStage = HeroWarriorAttackStage.NONE;
                break;
            case HeroWarriorAttackStage.SECONDATTACK:
                break;
            case HeroWarriorAttackStage.THIRDATTACK:
                break;
            case HeroWarriorAttackStage.FORTHATTACK:
                break;
        }

        if (!gameEntity.CacheAnimation.IsPlaying("Attack"))
        {
            gameEntity.stateMachine.ChangeState(HeroWarriorIdleState.Instance());
        }
    }

    public override void Exit(HeroWarriorEntityBeh gameEntity)
    {
        base.Exit(gameEntity);
    }
}
