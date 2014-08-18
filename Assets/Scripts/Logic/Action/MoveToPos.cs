using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Variables;

namespace NodeCanvas.Actions
{
    [Category("Unit")]
    [AgentType(typeof(AbsGameEntity))]
    public class MoveToPos : ActionTask
    {
        [RequiredField]
        public BBVector destination = null;

        private AbsGameEntity gameEntity = null;
        private Vector3 desPos = Vector3.zero;
        private float speed = 0f;

        protected override string OnInit()
        {
            this.gameEntity = (AbsGameEntity)base.agent;
            return null;
        }

        protected override void OnExecute()
        {
            this.speed = this.blackboard.GetDataValue<float>("Speed");
        }

        protected override void OnUpdate()
        {
            //this.desPos = new Vector3(this.destination.value.x,
            //    this.gameEntity.GetGameEntityTrans().position.y,
            //    this.destination.value.z);

            
            //this.gameEntity.GetGameEntityTrans().LookAt(this.desPos);
            ////this.gameEntity.GetGameEntityTrans().Translate(Vector3.forward * this.speed * Time.deltaTime);


            //float step = this.speed * Time.deltaTime;
            //this.gameEntity.GetGameEntityTrans().position = Vector3.MoveTowards(this.gameEntity.GetGameEntityTrans().position,
            //    this.desPos, step);

            //float distance = Vector3.Distance(this.unit.CacheTransform.position, 
            //    this.desPos);
            //if (distance <= this.unit.retainDistance)
            //    EndAction(true);
        }
    }
}
