using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    move,
    idle,
    yun,
    rush
};
public class rino : Enemy//继承基类Enemy
{
    private States zhuangTai;
    private float rushTime=5f;
    private float lftTime;
    protected override void Awake()
    {
        base.Awake();
        lftTime = rushTime;
        xunLuoState = new RinoXunLuoState();
    }

    public States ZhuangTai
    {
        get => zhuangTai;
        set
        {
            if(zhuangTai==value)return;
            if (value == States.move)
            {
                
            }

            if (value == States.idle)
            {
                anim.SetBool("isRush",false);
                Invoke("zhuanXiang",2f);
            }

            if (value == States.yun)
            {
                anim.Play("hitwall");
                Invoke("zhuanXiang",2f);
            }

            if (value == States.rush)
            {
                
            }
            zhuangTai = value;
        }
    }

    

    public override void Move()
    {
        if (_physicsCheck.noXuanya&&!_physicsCheck.isWall)
        {
            ZhuangTai = States.move;
            base.Move();
            anim.SetBool("isRush",true);
        }
        else if(!_physicsCheck.noXuanya&&!_physicsCheck.isWall)
        {
            ZhuangTai = States.idle;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        else if (_physicsCheck.isWall)
        {
            ZhuangTai = States.idle;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    public void zhuanXiang()
    {
        transform.localScale =new Vector3(-transform.localScale.x,1,1);
        _physicsCheck.pianYiXy = new Vector2(-_physicsCheck.pianYiXy.x, _physicsCheck.pianYiXy.y);
        _physicsCheck.pianYiWall = new Vector2(-_physicsCheck.pianYiWall.x, _physicsCheck.pianYiWall.y);
        _physicsCheck.pianYiTg = new Vector2(-_physicsCheck.pianYiTg.x, _physicsCheck.pianYiTg.y);
    }

    public void backIdle()
    {
        anim.Play("idle");
    }
}
