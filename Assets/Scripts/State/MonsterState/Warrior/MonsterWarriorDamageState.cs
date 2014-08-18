using UnityEngine;
using System.Collections;

public class MonsterWarriorDamageState : AbsState<MonsterWarriorEntityBeh>
{
    private static MonsterWarriorDamageState inst = null;
    public static MonsterWarriorDamageState Instance()
    {
        if (inst == null)
            inst = new MonsterWarriorDamageState();
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
