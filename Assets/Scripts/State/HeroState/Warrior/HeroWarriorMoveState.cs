using UnityEngine;
using System.Collections;
using whitecat.input;

public class HeroWarriorMoveState : AbsState<HeroWarriorEntityBeh>
{
    private static HeroWarriorMoveState inst = null;
    public static HeroWarriorMoveState Instance()
    {
        if (inst == null)
            inst = new HeroWarriorMoveState();
        return inst;
    }

    public override void Enter(HeroWarriorEntityBeh gameEntity)
    {
        base.Enter(gameEntity);
    }

    public override void Execute(HeroWarriorEntityBeh gameEntity)
    {
        float step = gameEntity.Speed * Time.deltaTime;
        switch (gameEntity.CurMoveState)
        {
            case MoveState.IDLE:
                gameEntity.stateMachine.ChangeState(HeroWarriorIdleState.Instance());
                break;
            case MoveState.WALK:
                gameEntity.CacheAnimation.wrapMode = WrapMode.Loop;
                gameEntity.CacheAnimation.CrossFade("Run", 0.3f, PlayMode.StopAll);
                gameEntity.CacheTransform.LookAt(gameEntity.DesPos);
                
                gameEntity.CacheTransform.position = Vector3.MoveTowards(gameEntity.CacheTransform.position,
                    gameEntity.DesPos, step);
                break;
            case MoveState.RUN:
                gameEntity.CacheAnimation.wrapMode = WrapMode.Loop;
                gameEntity.CacheAnimation.CrossFade("Run", 0.3f, PlayMode.StopAll);
                gameEntity.CacheTransform.LookAt(gameEntity.DesPos);

                gameEntity.CacheTransform.position = Vector3.MoveTowards(gameEntity.CacheTransform.position,
                    gameEntity.DesPos, step);
                break;
            case MoveState.ROLL:
                break;
            default:
                Debug.LogError("Error MoveState!!!");
                break;
        }
    }

    public override void Exit(HeroWarriorEntityBeh gameEntity)
    {
        base.Exit(gameEntity);
    }
}
