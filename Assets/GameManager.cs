using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float startTime;
    private void OnEnable()
    {
        EventBus.HitOnHpEvent += CompareHitOnHpJk;
        EventBus.HitOnHitEvent += CompareHitOnHitJk;
    }

    private void OnDisable()
    {
        EventBus.HitOnHpEvent -= CompareHitOnHpJk;
        EventBus.HitOnHitEvent -= CompareHitOnHitJk; ;
    }


    public void Start()
    {
        startTime = Time.time;
    }
    public void killself() 
    {
        if (Time.time - startTime >= 5f) 
        {
            GameObject pl = GameObject.FindWithTag("Player");
            if (pl != null)
            {
                pl.GetComponent<Juk_controller>().JukDeath();
            }
        }
    }

    public void CompareHitOnHpJk(Juk_controller jk1, Juk_controller jk2) 
    {
        if (jk1._lvl > jk2._lvl) 
        {
            Destroy(jk2.gameObject);
            jk1.SoundEat();
            if (jk1.GetComponent<PlayerMovement>().PlayerControlled == true)
            {
                FindObjectOfType<Manager>().Score += jk2._score;
            }
        }
        if (jk1._lvl <= jk2._lvl)
        {
            jk2._hp -= jk1._damage;
            jk1.pl_mov.ThrowingAfterHit(jk2.transform.position);
            jk1.SoundBeat();
            if(jk2._hp <= 0) 
            {
                if (jk1.GetComponent<PlayerMovement>().PlayerControlled == true)
                {
                    FindObjectOfType<Manager>().Score += jk2._score;
                }
            }
            FsmExample _fsmexample = jk2.gameObject.GetComponent<FsmExample>();
            if (_fsmexample != null)
            {
                if(jk2.is_stuned == false) 
                {
                    _fsmexample.SetStateAgr();
                }
            }
        }
    }
    public void CompareHitOnHitJk(Juk_controller jk1, Juk_controller jk2)
    {
        if (jk1._lvl > jk2._lvl)
        {
            Destroy(jk2.gameObject);
            jk1.SoundEat();
            if (jk1.GetComponent<PlayerMovement>().PlayerControlled == true)
            {
                FindObjectOfType<Manager>().Score += jk2._score;
            }
        }
        if (jk1._lvl <= jk2._lvl)
        {
            jk1.SoundBeat();
            jk1.pl_mov.ThrowingAfterHit(jk2.transform.position);
            jk1._hp -= jk2._damage/2;
            jk2._hp -= jk2._damage/2;
            if (jk1.pl_mov.PlayerControlled == false) 
            {
                jk1.is_stuned = true;
                jk1.StunImage.SetBool("stunning", true);
            }
            FsmExample _fsmexample = jk2.gameObject.GetComponent<FsmExample>();
            if (_fsmexample != null)
            {
                if (jk2.is_stuned == false)
                {
                    _fsmexample.SetStateAgr();
                }
            }

        }
    }
}
