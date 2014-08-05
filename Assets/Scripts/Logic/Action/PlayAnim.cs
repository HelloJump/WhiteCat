using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NodeCanvas.Actions
{
    [Category("Unit")]
    [AgentType(typeof(Animation))]
    public class PlayAnim : ActionTask
    {
        [RequiredField]
        public AnimationClip animClip = null;
        [SliderField(0f, 1f)]
        public float crossFade = 0.3f;
        public WrapMode wrapMode = WrapMode.Default;

        private Animation anim = null;

        protected override string OnInit()
        {
            this.anim = (Animation)base.agent;
            return null;
        }

        protected override void OnExecute()
        {
            this.anim.wrapMode = this.wrapMode;
            this.anim.CrossFade(this.animClip.name, this.crossFade);
        }

        protected override void OnUpdate()
        {
            if ((this.wrapMode == WrapMode.Loop) ||
                !this.anim.IsPlaying(this.animClip.name))
                EndAction(true);
        }
    }
}
