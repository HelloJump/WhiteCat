using UnityEngine;
using System.Collections;
/// <summary>
/// Just for script
/// </summary>
public class FightCommon : MonoBehaviour 
{
    private static FightCommon inst = null;

    public static FightCommon Instance()
    {
        return inst;
    }

    void Awake()
    {
        inst = this;
    }

    void OnDestroy()
    {
        inst = null;
    }

    public float GetDistance(Transform tran1, Transform tran2)
    {
        return Mathf.Abs(tran1.localPosition.sqrMagnitude - tran2.localPosition.sqrMagnitude);
    }
}
