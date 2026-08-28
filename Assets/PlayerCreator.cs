using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    public Juk_controller juk_controller;
    public GameObject[] juks;
    void Awake()
    {
        if (Stats_class.lvl != 999)
        {
            GameObject juk_player = Instantiate(juks[Stats_class.lvl - 1], new Vector3(315.799988f, -0.5f, 280.100006f), Quaternion.identity);
            juk_controller = juk_player.GetComponent<Juk_controller>();

            juk_controller._lvl = Stats_class.lvl;
            juk_controller._damage = Stats_class.damage;
            juk_controller._Speed = Stats_class.speed;
            juk_controller._RotationSpeed = Stats_class.rotationspeed;
            juk_controller._maxhp = Stats_class.health;
            juk_controller._hp = Stats_class.health;
        }
        else 
        {
            GameObject juk_player = Instantiate(juks[5], new Vector3(315.799988f, -0.5f, 280.100006f), Quaternion.identity);
            juk_controller = juk_player.GetComponent<Juk_controller>();

            juk_controller._lvl = 999;
            juk_controller._damage = Stats_class.damage;
            juk_controller._Speed = 40f;
            juk_controller._RotationSpeed = 1500f;
            juk_controller._maxhp = 1000f;
            juk_controller._hp = 1000f;

            FindObjectOfType<Camera>().gameObject.GetComponent<CameraFollow>().offset = new Vector3(30f, 45f, 0f);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
