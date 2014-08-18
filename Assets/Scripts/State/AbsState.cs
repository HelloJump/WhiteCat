using UnityEngine;
using System.Collections;

public class AbsState<T>
{
    virtual public void Enter(T gameEntity)
    { 

    }

    virtual public void Execute(T gameEntity)
    { 
    }

    virtual public void Exit(T gameEntity)
    {
 
    }
}
