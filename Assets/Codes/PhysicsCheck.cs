using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("地面检测")]
    public float radius;
    public Vector2 pianYi;
    public Vector2 size;
    public CapsuleDirection2D dirctionCap;
    public LayerMask ground;
    public LayerMask trap;
    public bool isOnGround;
    public bool isOnTrap;
    [Header("悬崖检测")]
    public bool noXuanya;
    public Vector2 pianYiXy;
    public float ballRadius;
    [Header("墙体检测")]
    public bool isWall;
    public Vector2 pianYiWall;
    public float wallRadius;
    [Header("敌人检测")]
    public bool isTarget;
    public Vector2 pianYiTg;
    public Vector2 sizeTg;
    public CapsuleDirection2D dirctionCapTg;
    public LayerMask players;
    private void Update()
    {
        checkIt();
    }

    void checkIt()    {
        isOnGround = Physics2D.OverlapCapsule((Vector2)transform.position+pianYi, size,dirctionCap,0,ground);
        isOnTrap=Physics2D.OverlapCapsule((Vector2)transform.position+pianYi, size,dirctionCap,0,trap);
        noXuanya = Physics2D.OverlapCircle((Vector2)transform.position+pianYiXy,ballRadius,ground);
        isWall = Physics2D.OverlapCircle((Vector2)transform.position+pianYiWall,wallRadius,ground);
        isTarget= Physics2D.OverlapCapsule((Vector2)transform.position+pianYiTg, sizeTg,dirctionCapTg,0,players);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube((Vector2)transform.position+pianYi,size);
        Gizmos.DrawSphere((Vector2)transform.position+pianYiXy,ballRadius);
        Gizmos.DrawSphere((Vector2)transform.position+pianYiWall,wallRadius);
        Gizmos.DrawCube((Vector2)transform.position+pianYiTg,sizeTg);
    }
}
