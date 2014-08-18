using UnityEngine;
using System.Collections;

public class MonsterWarriorloiterState : AbsState<MonsterWarriorEntityBeh>
{
    private static MonsterWarriorloiterState inst = null;
    public static MonsterWarriorloiterState Instance()
    {
        if (inst == null)
            inst = new MonsterWarriorloiterState();
        return inst;
    }

    public override void Enter(MonsterWarriorEntityBeh gameEntity)
    {
        base.Enter(gameEntity);
    }

    public override void Execute(MonsterWarriorEntityBeh gameEntity)
    {
        gameEntity.CacheAnimation.wrapMode = WrapMode.Loop;
        gameEntity.CacheAnimation.CrossFade("Walk", 0.3f, PlayMode.StopAll);

        if (gameEntity.HasValidTarget())
        {
            gameEntity.stateMachine.ChangeState(MonsterWarriorAttackState.Instance());
        }
    }

    public override void Exit(MonsterWarriorEntityBeh gameEntity)
    {
        base.Exit(gameEntity);
    }
}
