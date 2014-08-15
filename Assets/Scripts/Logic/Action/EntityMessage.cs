using UnityEngine;
using System.Collections;
using NodeCanvas;

namespace NodeCanvas.Actions
{
    public class EntityMessage : ActionTask
    {
        AbsGameEntity gameEntity = null;
        protected override string OnInit()
        {
            this.gameEntity = (AbsGameEntity)agent;
            return null;
        }

        protected override void OnExecute()
        {
            //this.gameEntity.HandleMessage()
        }
    }
}