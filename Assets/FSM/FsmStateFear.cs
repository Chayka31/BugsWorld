using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using UnityEngine.UIElements;
using System.Drawing;

namespace FSM
{
    public class FsmStateFear : FsmState
    {
        protected readonly Transform _transform;
        protected readonly Transform _transformtelce;
        protected readonly float _radius;
        protected readonly float _speed;
        protected readonly float _rotationspeed;
        protected readonly Juk_controller _juk_this;
        public Juk_controller _juk_nearest;
        public FsmExample _example;

        public FsmStateFear(Fsm fsm, Transform transform, float radius, float speed, float rotationspeed, Transform transformtelce, Juk_controller juk_this, Juk_controller juk_player, FsmExample example) : base(fsm)
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
            Debug.Log("FearEnter");
        }

        public override void Exit()
        {
            Debug.Log("FearExit");
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
                    Vector3 directionm = _juk_nearest.transform.position - _transform.position;
                    Vector3 OpositDir = -directionm.normalized;
                    _transform.position += OpositDir * Time.deltaTime * _speed;

                    Vector3 direction = _juk_nearest.transform.position - _transform.position;
                    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Вычисляем угол поворота по оси Y
                    angle += 180;
                    // Поворачиваем объект с заданной скоростью
                    float currentAngle = _transformtelce.transform.eulerAngles.y; // Текущий угол поворота по оси Y
                    float targetAngle = Mathf.MoveTowardsAngle(currentAngle, angle, _rotationspeed * Time.deltaTime);
                    _transformtelce.rotation = Quaternion.Euler(0f, targetAngle, 0f); // Поворачиваем объект только по оси Y
                }
            }
        }
    }
}

