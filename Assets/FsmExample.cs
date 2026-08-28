using FSM;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FsmExample : MonoBehaviour
{
    private Fsm _fsm;
    private Juk_controller controller;
    public float RadiusCheck;
    public Juk_controller closestJuk;
    void Awake()
    {
        _fsm = new Fsm();
        controller = GetComponent<Juk_controller>();
        _fsm.AddState(new FsmStateIdle(_fsm, transform,RadiusCheck,controller._Speed, controller._RotationSpeed, controller.Telce.transform, controller, GameObject.FindGameObjectWithTag("Player").GetComponent<Juk_controller>(),this));
        _fsm.AddState(new FsmStateFear(_fsm, transform, RadiusCheck, controller._Speed, controller._RotationSpeed, controller.Telce.transform, controller, GameObject.FindGameObjectWithTag("Player").GetComponent<Juk_controller>(),this));
        _fsm.AddState(new FsmStateAgr(_fsm, transform, RadiusCheck, controller._Speed, controller._RotationSpeed, controller.Telce.transform, controller, GameObject.FindGameObjectWithTag("Player").GetComponent<Juk_controller>(),this));
        _fsm.AddState(new FsmStateStun(_fsm, transform, RadiusCheck, controller._Speed, controller._RotationSpeed, controller.Telce.transform, controller, GameObject.FindGameObjectWithTag("Player").GetComponent<Juk_controller>()));
        _fsm.SetState<FsmStateIdle>(); 
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.Update();
        FindClosestJuk();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(155, 155, 155, 0.5f);
        Gizmos.DrawSphere(transform.position, RadiusCheck);
    }

    public void SetStateAgr() 
    {
        _fsm.SetState<FsmStateAgr>();
    }

    private void FindClosestJuk()
    {
        // Получить всех жуков в сцене
        Juk_controller[] allJuks = FindObjectsOfType<Juk_controller>();

        // Инициализация минимального расстояния и ближайшего жука
        float minDistance = Mathf.Infinity;
        closestJuk = null;

        // Пройтись по всем жукам
        foreach (Juk_controller juk in allJuks)
        {
            // Пропускаем себя
            if (juk == GetComponent<Juk_controller>())
            {
                continue;
            }

            // Получить расстояние до жука
            float distance = Vector3.Distance(transform.position, juk.transform.position);

            // Обновить минимальное расстояние и ближайшего жука, если найдено более близкое расстояние
            if (distance < minDistance && distance <= RadiusCheck)
            {
                minDistance = distance;
                closestJuk = juk;
            }
        }
    }
}
