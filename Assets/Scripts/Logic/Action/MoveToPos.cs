using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Variables;

namespace NodeCanvas.Actions
{
    [Category("Unit")]
    [AgentType(typeof(AbsLifeUnitBeh))]
    public class MoveToPos : ActionTask
    {
        [RequiredField]
        public BBVector destination = null;

        private AbsLifeUnitBeh unit = null;
        private Vector3 desPos = Vector3.zero;

        protected override string OnInit()
        {
            this.unit = (AbsLifeUnitBeh)base.agent;
            return null;
        }

        protected override void OnExecute()
        {
            this.desPos = new Vector3(this.destination.value.x, 
                this.unit.CacheTransform.position.y,
                this.destination.value.z);
            this.unit.CacheTransform.LookAt(this.desPos);
        }

        protected override void OnUpdate()
        {
            this.desPos = new Vector3(this.destination.value.x,
                this.unit.CacheTransform.position.y,
                this.destination.value.z);
            this.unit.CacheTransform.LookAt(this.desPos);

            float step = this.unit.speed * Time.deltaTime;
            this.unit.CacheTransform.position = Vector3.MoveTowards(this.unit.CacheTransform.position,
                this.desPos, step);

            float distance = Vector3.Distance(this.unit.CacheTransform.position, 
                this.desPos);
            if (distance <= this.unit.retainDistance)
                EndAction(true);
        }
    }
}
