using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using System.Threading;

public class FsmStateStun : FsmState
{
    protected readonly Transform _transform;
    protected readonly Transform _transformtelce;
    protected readonly float _radius;
    protected readonly float _speed;
    protected readonly float _rotationspeed;
    protected readonly Juk_controller _juk_this;
    protected readonly Juk_controller _juk_player;

    public float timestun;


    private Timer _timer;
    public FsmStateStun(Fsm fsm, Transform transform, float radius, float speed, float rotationspeed, Transform transformtelce, Juk_controller juk_this, Juk_controller juk_player) : base(fsm)
    {
        _transform = transform;
        _transformtelce = transformtelce;
        _radius = radius;
        _speed = speed;
        _rotationspeed = rotationspeed;
        _juk_this = juk_this;
        _juk_player = juk_player;
    }

    public override void Enter()
    {
        Debug.Log("StunEnter");
        timestun = 0;
    }
    public override void Exit()
    {
        Debug.Log("StunExit");
    }
    public override void Update()
    {

        timestun++;

        if (timestun >= 250) 
        {
            stuning();
        }
    }
    public void stuning() 
    {
        _juk_this.is_stuned = false;
        _juk_this.StunImage.SetBool("stunning", false);
        Fsm.SetState<FsmStateAgr>();
    }
}
