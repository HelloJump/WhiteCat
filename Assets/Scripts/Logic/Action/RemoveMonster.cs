using UnityEngine;
using System.Collections;
using NodeCanvas.Variables;

namespace NodeCanvas.Actions
{
    [Category("Monster")]
    public class RemoveMonster : ActionTask
    {
        protected override void OnExecute()
        {
            gameObject.SendMessage("MonsterDie");
        }
    }
}
