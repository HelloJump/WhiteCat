using UnityEngine;
using System.Collections;

public class HeroWarriorDieState : AbsState<MonsterWarriorEntityBeh>
{
    private static HeroWarriorDieState inst = null;

    public static HeroWarriorDieState Instance()
    {
        if (inst == null)
            inst = new HeroWarriorDieState();
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
