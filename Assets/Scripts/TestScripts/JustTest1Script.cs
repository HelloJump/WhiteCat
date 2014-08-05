using UnityEngine;
using System.Collections;
using NodeCanvas.Variables;

namespace NodeCanvas.Actions
{
    [Category("JustTest")]
    public class JustTest1Script : ActionTask
    {
        protected override void OnExecute()
        {
            Debug.Log("Execute JustTest1Script");
        }
    }
}

