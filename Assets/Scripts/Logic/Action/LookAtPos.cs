using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Variables;

namespace NodeCanvas.Actions
{
    [Category("Logic")]
    public class LookAtPos : ActionTask
    {
        [RequiredField]
        public BBVector pos = null;

        private Transform cacheTrans = null;

        protected override string OnInit()
        {
            this.cacheTrans = agent.transform;
            return null;
        }

        protected override void OnExecute()
        {
            Debug.Log(this.cacheTrans.name + ":" + this.pos.value + "!!!!!!!!!!!!");
            this.cacheTrans.LookAt(this.pos.value);
            EndAction(true);
        }
    }
}
