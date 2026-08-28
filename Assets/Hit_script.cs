using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hit_script : MonoBehaviour
{
    public Juk_controller controller;
    void Start()
    {
        controller = transform.parent.transform.parent.parent.GetComponent<Juk_controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag) 
        {
            case "HpCollider": EventBus.HitOnHpEvent(controller, other.gameObject.GetComponent<Hp_Script>().controller1); break;
            case "HitCollider": EventBus.HitOnHitEvent(controller, other.gameObject.GetComponent<Hit_script>().controller); break;
        }
    }
}
