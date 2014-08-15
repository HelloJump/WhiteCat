using UnityEngine;
using System.Collections;
using NodeCanvas;

namespace NodeCanvas.Actions
{
    [Category("Logic")]
    public class AddDeltaTime : ActionTask
    {
        protected override void OnUpdate()
        {
            float attackTime = this.blackboard.GetDataValue<float>("AttackTime");
            attackTime += Time.deltaTime;
            this.blackboard.SetDataValue("AttackTime", attackTime);
        }
    }
}

