using UnityEngine;
using System.Collections;

public class HeroWarriorDamageState : AbsState<HeroWarriorEntityBeh> 
{
    private static HeroWarriorDamageState inst = null;
    public static HeroWarriorDamageState Instance()
    {
        if (inst == null)
            inst = new HeroWarriorDamageState();
        return inst;
    }

    public override void Enter(HeroWarriorEntityBeh gameEntity)
    {
        base.Enter(gameEntity);
    }

    public override void Execute(HeroWarriorEntityBeh gameEntity)
    {
        base.Execute(gameEntity);
    }

    public override void Exit(HeroWarriorEntityBeh gameEntity)
    {
        base.Exit(gameEntity);
    }
}
