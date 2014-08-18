using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using whitecat.input;
using NodeCanvas;
using NodeCanvas.StateMachines;


public class AbsGameEntity : MonoBehaviour 
{
    public FSM fsm = null;
    public Blackboard bb = null;

    protected Stack<string> connectionCache = new Stack<string>();

    private Transform cacheTransform;
    public Transform CacheTransform
    {
        get { return this.cacheTransform; }
    }

    private Animation cacheAnimation;
    public Animation CacheAnimation
    {
        get { return this.cacheAnimation; }
    }

    public virtual void HandleMessage(Message message)
    {

    }

    public virtual void Awake()
    {
        //this.fsm = GetComponent<FSMOwner>().FSM;
        //this.bb = GetComponent<Blackboard>();
        this.cacheTransform = transform;
        this.cacheAnimation = GetComponent<Animation>();
    }

    private MoveState curMoveState = MoveState.IDLE;
    public MoveState CurMoveState
    {
        get { return this.curMoveState; }
        set { this.curMoveState = value; }
    }

    private Vector3 desPos = Vector3.zero;
    public Vector3 DesPos
    {
        get { return this.desPos; }
        set { this.desPos = value; }
    }

    private float speed = 1f;
    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    public virtual bool HasValidTarget()
    {
        return false;
    }



    void Start()
    {
        EntityManager.Instance().RegisterEntity(this);
    }

    public virtual void Update()
    {
 
    }

    public virtual void AttackListener()
    {

    }

    
}
