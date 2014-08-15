using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageDispatcher : MonoBehaviour
{
    void Awake()
    {
        inst = this;
    }

    void OnDestroy()
    {
        inst = null;
    }

    private static MessageDispatcher inst = null;
    public static MessageDispatcher Instance()
    {
        return inst;
    }

    private List<Message> delayedMsg = new List<Message>();

    public void DispatchMessage(int msgId, int senderId, int receiverId, object extraInfo, float dispatchTime)
    {
        AbsGameEntity gameEntity = EntityManager.Instance().GetEntityById(receiverId);
        Message message = new Message(msgId, senderId, receiverId, extraInfo, dispatchTime);
        gameEntity.HandleMessage(message);
    }

    public void DispathcDelayMessage(int msgId, int senderId, int receiverId, object extraInfo, float dispatchTime)
    {
        Message message = new Message(msgId, senderId, receiverId, extraInfo, dispatchTime);
        this.delayedMsg.Add(message);
    }

    void Update()
    {
        List<int> removeIdx = new List<int>();

        for (int i = this.delayedMsg.Count; i >= 0; i--)
        {
            AbsGameEntity gameEntity = EntityManager.Instance().GetEntityById(this.delayedMsg[i].receiverId);
            this.delayedMsg[i].dispatchTime -= Time.deltaTime;
            if (this.delayedMsg[i].dispatchTime <= 0)
            {
                gameEntity.HandleMessage(this.delayedMsg[i]);
                removeIdx.Add(i);
            }
        }

        for (int i = 0; i < removeIdx.Count; i++)
        {
            this.delayedMsg.RemoveAt(removeIdx[i]);
        }
    }
}
