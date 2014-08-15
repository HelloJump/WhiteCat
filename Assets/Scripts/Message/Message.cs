using UnityEngine;
using System.Collections;

public class Message
{
    public const int attackMsgId = 100001;

    public int senderId;
    public int receiverId;
    public int msgId;
    public object extraInfo;
    public float dispatchTime;

    public Message(int msgId, int senderId, int receiverId, object extraInfo, float dispatchTime)
    {
        this.msgId = msgId;
        this.senderId = senderId;
        this.receiverId = receiverId;
        this.extraInfo = extraInfo;
        this.dispatchTime = dispatchTime;
    }
}
