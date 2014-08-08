using UnityEngine;
using System.Collections;

public class AbsUnitBeh : MonoBehaviour 
{
    // Component
    protected Transform cacheTransform = null;

    // Getter, Setter
    public virtual Transform CacheTransform
    {
        get { return this.cacheTransform; }
    }

    // MonoBehaviour
    protected virtual void Awake()
    {
        this.cacheTransform = transform;
    }
}
