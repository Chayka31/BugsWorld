using UnityEngine;
using UnityEngine.UIElements;

namespace FSM
{
    public class FsmStateIdle : FsmState
    {
        protected readonly Transform _transform;
        protected readonly Transform _transformtelce;
        protected readonly float _radius;
        protected readonly float _speed;
        protected readonly float _rotationspeed;
        protected readonly Juk_controller _juk_this;
        public Juk_controller _juk_nearest;
        public FsmExample _example;

        public float timetoskippoint;

        protected Vector3 point;
        public FsmStateIdle(Fsm fsm, Transform transform, float radius, float speed, float rotationspeed, Transform transformtelce, Juk_controller juk_this, Juk_controller juk_player, FsmExample example) : base(fsm) 
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
            Debug.Log("Enter_Idle");
            RandomizePoint();
        }


        public override void Exit()
        {
             Debug.Log("Exit_Idle");
        }

        public override void Update()
        {
            _juk_nearest = _example.closestJuk;
            if (_juk_nearest != null) 
            {
                float distance = Vector3.Distance(_transform.position, _juk_nearest.transform.position);
                if (_juk_this._lvl < _juk_nearest._lvl)
                {
                    Fsm.SetState<FsmStateFear>();
                }
                if (_juk_this._lvl > _juk_nearest._lvl)
                {
                    Fsm.SetState<FsmStateAgr>();
                }
            }
            timetoskippoint += 0.001f;

            if (timetoskippoint >= 1) 
            {
                RandomizePoint();
                Debug.Log("новый поинт из за недосягаемости");
                timetoskippoint = 0;
            }
            if (Vector3.Distance(_transform.position, point) < 1)
            {
                RandomizePoint();
                timetoskippoint = 0;
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, point, Time.deltaTime * _speed);

                Vector3 direction = point - _transform.position;
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Вычисляем угол поворота по оси Y
                float currentAngle = _transformtelce.eulerAngles.y; // Текущий угол поворота по оси Y
                float targetAngle = Mathf.MoveTowardsAngle(currentAngle, angle, _rotationspeed * Time.deltaTime);
                _transformtelce.rotation = Quaternion.Euler(0f, targetAngle, 0f); // Поворачиваем объект только по оси Y

            }
            if (_juk_this.is_stuned)
            {
                Fsm.SetState<FsmStateStun>();
            }

        }

        public void RandomizePoint() 
        {
            float randomangle = Random.Range(0, 2 * Mathf.PI);
            float x = _transform.position.x + _radius * Mathf.Cos(randomangle);
            float z = _transform.position.z + _radius * Mathf.Sin(randomangle);
            point = new Vector3( x, _transform.position.y, z);
        }
    }
}
