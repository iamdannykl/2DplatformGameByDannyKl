using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoXunLuoState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;//在onEnable()法里传入当前的Enemy对象
    }

    public override void LogicUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public override void PhysicsUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        //throw new System.NotImplementedException();
    }
}
