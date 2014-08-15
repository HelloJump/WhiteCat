using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroEntityBeh : AbsGameEntity
{
    public const int msgAttackFirst = 10001;

    public override void HandleMessage(Message message)
    {
        base.HandleMessage(message);
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void AttackListener()
    {
        int attackIdx = (int)this.bb.GetDataValue<float>("AttackIdx");
        int currentIdx = (int)this.bb.GetDataValue<float>("CurrentIdx");
        switch (attackIdx)
        {
            case 0:
                this.bb.SetDataValue("AttackIdx", currentIdx + 1f);
                break;
            case 1:
                this.bb.SetDataValue("AttackIdx", currentIdx + 1f);
                break;
            case 2:
                this.bb.SetDataValue("AttackIdx", currentIdx + 1f);
                break;
            case 3:
                this.bb.SetDataValue("AttackIdx", currentIdx + 1f);
                break;
            default:
                break;
        }
    }

    void IdleListener()
    {
        Debug.Log("Idle!!!");
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
