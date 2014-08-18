using UnityEngine;
using System.Collections;

namespace whitecat.input
{
    public enum MoveState
    {
        IDLE,
        WALK,
        RUN,
        ROLL
    }

    public class ScreenInputBeh : AbsInputBeh
    {
        private const float rollCheckTime = 0.5f;
        private const float rollCheckSpeed = 100f;
        private const float walkCheckOffset = 50f;
        private const float skillCheckOffset = 10f;
        private const float skillCheckTime = 1f;

        

        public GameObject InputMessageReceiver;
        void OnClick()
        {
            if (InputMessageReceiver == null)
            {
                Debug.LogWarning("InputMessageReceiver is null");
            }
            InputMessageReceiver.SendMessage("AttackListener");
        }

        private Vector2 endOffsetVec2 = Vector2.zero;
        void OnDrag(Vector2 delta)
        {
            endOffsetVec2 += delta;
        }

        private float pressTime = 0f;
        private bool isPress = false;
        void OnPress(bool isPress)
        {
            this.isPress = isPress;
            if (isPress)
            {
                pressTime = 0;
                endOffsetVec2 = Vector2.zero;
            }
            else
            {
                if (pressTime < rollCheckTime && !this.isPress && Mathf.Sqrt(endOffsetVec2.SqrMagnitude()) > rollCheckSpeed)
                {
                    InputMessageReceiver.SendMessage("MoveListener", MoveState.ROLL);
                    InputMessageReceiver.SendMessage("MoveDestinationListener", endOffsetVec2);
                }
                pressTime = 0;
                skillTime = 0;
                InputMessageReceiver.SendMessage("IdleListener");
            }
        }

        private float skillTime = 0f;
        void Update()
        {
            pressTime += Time.deltaTime;
            float endOffset = Mathf.Sqrt(endOffsetVec2.SqrMagnitude());
            if (endOffset < skillCheckOffset && this.isPress)
                skillTime += Time.deltaTime;
            else
                skillTime = 0f;

            if (skillTime >= skillCheckTime && this.isPress)
            {
                InputMessageReceiver.SendMessage("SkillStateListener");
                Debug.Log("SkillStateListener");
            }

            if (this.isPress && this.pressTime >= rollCheckTime)
            {
                if (endOffset <= skillCheckOffset && skillTime >= skillCheckTime)
                {
                    InputMessageReceiver.SendMessage("StagnantListener");
                    Debug.Log("StagnantListener");
                }
                else if (endOffset > skillCheckOffset && endOffset <= walkCheckOffset)
                {
                    skillTime = 0;
                    InputMessageReceiver.SendMessage("MoveListener", MoveState.WALK);
                    InputMessageReceiver.SendMessage("MoveDestinationListener", endOffsetVec2);
                }
                else if (endOffset > walkCheckOffset)
                {
                    skillTime = 0;
                    InputMessageReceiver.SendMessage("MoveListener", MoveState.RUN);
                    InputMessageReceiver.SendMessage("MoveDestinationListener", endOffsetVec2);
                }
            }
        }
    }
}
