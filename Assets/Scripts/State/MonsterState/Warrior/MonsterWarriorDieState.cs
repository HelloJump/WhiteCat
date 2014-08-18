using UnityEngine;
using System.Collections;

public class MonsterWarriorDieState : AbsState<MonsterWarriorEntityBeh>
{
    private static MonsterWarriorDieState inst = null;
    public static MonsterWarriorDieState Instance()
    {
        if (inst == null)
            inst = new MonsterWarriorDieState();
        return inst;
    }

    public override void Enter(MonsterWarriorEntityBeh gameEntity)
    {
        base.Enter(gameEntity);
    }

    public override void Execute(MonsterWarriorEntityBeh gameEntity)
    {
        base.Execute(gameEntity);
    }

    public override void Exit(MonsterWarriorEntityBeh gameEntity)
    {
        base.Exit(gameEntity);
    }
}
