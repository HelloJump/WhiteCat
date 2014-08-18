using UnityEngine;
using System.Collections;

public class MonsterWarriorAttackState : AbsState<MonsterWarriorEntityBeh>
{
    private static MonsterWarriorAttackState inst = null;
    public static MonsterWarriorAttackState Instance()
    {
        if (inst == null)
            inst = new MonsterWarriorAttackState();
        return inst;
    }

    public override void Enter(MonsterWarriorEntityBeh gameEntity)
    {
        base.Enter(gameEntity);
    }

    public override void Execute(MonsterWarriorEntityBeh gameEntity)
    {
        gameEntity.CacheAnimation.wrapMode = WrapMode.Once;
        gameEntity.CacheAnimation.CrossFade("Attack", 0.3f, PlayMode.StopAll);

        if (!gameEntity.CacheAnimation.IsPlaying("Attack"))
        {
            gameEntity.stateMachine.ChangeState(MonsterWarriorloiterState.Instance());
        }

        base.Execute(gameEntity);
    }

    public override void Exit(MonsterWarriorEntityBeh gameEntity)
    {
        base.Exit(gameEntity);
    }
}
