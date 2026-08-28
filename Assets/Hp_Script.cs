using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp_Script : MonoBehaviour
{
    public Juk_controller controller1;
    void Start()
    {
        controller1 = transform.parent.parent.parent.GetComponent<Juk_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {

    }


}
