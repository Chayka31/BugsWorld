using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBus 
{
    public static Action<Juk_controller,Juk_controller> HitOnHpEvent;
    public static Action<Juk_controller,Juk_controller> HitOnHitEvent;
}
