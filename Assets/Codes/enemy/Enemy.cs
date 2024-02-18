using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("基本参数")] 
    public float nmSpeed;
    public float rushSpeed;
    public float currentSpd;
    public Vector2 chaoXiang;
    [Header("组件")] 
    protected Rigidbody2D rb;
    public Animator anim;
    public PhysicsCheck _physicsCheck;
    private BaseState currentState;
    protected BaseState xunLuoState;
    protected BaseState zhuJiState;

    protected virtual void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpd = nmSpeed;
        _physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void OnEnable()
    {
        currentState = xunLuoState;
        currentState.OnEnter(this);
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpd * chaoXiang.x*Time.deltaTime, rb.velocity.y);
    }

    protected virtual void Update()
    {
        chaoXiang = new Vector2(transform.localScale.x, 0);
        currentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        Move();
        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }
}
