using UnityEngine;
using System.Collections;

public class MonsterBeh : AbsLifeUnitBeh 
{
    void Start()
    {
        InvokeRepeating("SetDestinationPos", 1f, 2f);
    }

    void Update()
    { 

    }

    public override System.Collections.Generic.Dictionary<string, float> GetAttackTargetDistance()
    {
        return base.GetAttackTargetDistance();
    }

    private void SetDestinationPos()
    {
        Vector3 curPos = this.CacheTransform.localPosition;
        float x = Random.Range(-5f,5f);
        float z = Random.Range(-5f,5f);

        this.Bb.SetDataValue("DestinationPos", curPos + new Vector3(x,0,z));
    }

    void MonsterDie()
    {
        string monsterId = this.Bb.GetDataValue<string>("MonsterId");
        if (GameManagerBeh.Instance().monsterDict.ContainsKey(monsterId))
        {
            GameManagerBeh.Instance().monsterDict.Remove(monsterId);
        }
        Debug.Log("Monster " + monsterId + " Die!!!");
    }
}
