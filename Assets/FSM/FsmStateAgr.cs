using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FSM;

namespace FSM
{
    public class FsmStateAgr : FsmState
    {

        protected readonly Transform _transform;
        protected readonly Transform _transformtelce;
        protected readonly float _radius;
        protected readonly float _speed;
        protected readonly float _rotationspeed;
        protected readonly Juk_controller _juk_this;
        public Juk_controller _juk_nearest;
        public FsmExample _example;

        public FsmStateAgr(Fsm fsm, Transform transform, float radius, float speed, float rotationspeed, Transform transformtelce, Juk_controller juk_this, Juk_controller juk_player,FsmExample example) : base(fsm)
        {
            _transform = transform;
            _transformtelce = transformtelce;
            _radius = radius;
            _speed = speed;
            _rotationspeed = rotationspeed;
            _juk_this = juk_this;
            _juk_nearest = juk_player;
            _example = example;
        }

        public override void Enter()
        {
            Debug.Log("AgrEnter");
        }
        public override void Exit()
        {
            Debug.Log("AgrExit");
        }
        public override void Update()
        {
            _juk_nearest = _example.closestJuk;
            if (_juk_nearest == null)
            {
                Fsm.SetState<FsmStateIdle>();
            }
            else
            {
                float distance = Vector3.Distance(_transform.position, _juk_nearest.transform.position);
                if (_transform != null && _transformtelce != null)
                {
                    _transform.position = Vector3.MoveTowards(_transform.position, _juk_nearest.transform.position, Time.deltaTime * _speed);
                    Vector3 direction = (_juk_nearest.transform.position - _transform.position) * _radius;
                    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Вычисляем угол поворота по оси Y
                    float currentAngle = _transformtelce.eulerAngles.y; // Текущий угол поворота по оси Y
                    float targetAngle = Mathf.MoveTowardsAngle(currentAngle, angle, _rotationspeed * Time.deltaTime);
                    _transformtelce.rotation = Quaternion.Euler(0f, targetAngle, 0f); // Поворачиваем объект только по оси Y
                }
            }
            if (_juk_this.is_stuned) 
            {
                Fsm.SetState<FsmStateStun>();
            }
        }
    }
}
