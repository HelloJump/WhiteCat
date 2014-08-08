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

    protected override bool HasAttackTargetArround()
    {
        return base.HasAttackTargetArround();
    }

    private void SetDestinationPos()
    {
        Vector3 curPos = this.CacheTransform.localPosition;
        float x = Random.Range(-5f,5f);
        float z = Random.Range(-5f,5f);

        this.Bb.SetDataValue("DestinationPos", curPos + new Vector3(x,0,z));
    }
}
