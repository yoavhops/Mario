using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : Player
{

    private float dir = 0f;

    protected override void OnStart()
    {
        base.OnStart();
        dir = UnityEngine.Random.value * 2 - 1;
    }

    protected override float GetAxisRaw()
    {
        return dir;
    }

    protected bool GetButtonDown(string button)
    {
        return false;
    }

    protected virtual void OnUpdate()
    {
        base.OnUpdate();

        //TODO : check for dir change.. when hitting wall

    }
}
