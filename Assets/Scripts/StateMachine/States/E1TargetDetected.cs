using System;
using System.Collections.Generic;
using UnityEngine;

public class E1TargetDetected : IState
{
    AnimationHelper animHelper;
  Movement movement;
  FSM<E1TargetDetected> stateMachine;
  AI ai;
Shoot shoot;
Dodge dodge;
Roam chase;
public float startTime{get; set;}
    public E1TargetDetected(AI ai,Movement movement)
    {
        animHelper=ai.animationHelper;
        this.movement=movement;
        this.ai=ai;
        stateMachine=new FSM<E1TargetDetected>(this);
         shoot=new Shoot(ai,movement,this);
         dodge=new Dodge(ai,movement,this);
         chase=new Roam(ai,movement);
         stateMachine.AddTransition(shoot,dodge,canDodge());
         stateMachine.AddTransition(chase,dodge,canDodge());
         stateMachine.AddTransition(dodge,shoot,canShoot());
         stateMachine.AddTransition(shoot,chase,canChase());
         stateMachine.AddTransition(chase,shoot,canShoot());
         Func<bool> canDodge()=>()=> Physics2D.OverlapCircle(movement.playerCheck.transform.position,.1f,ai.playerLayerMask);
         Func<bool> canShoot()=>()=> ai.Grounded &&ai.target&&Mathf.Abs(movement.transform.position.x-ai.destination.x)<5f;
         Func<bool> canChase()=>()=> Mathf.Abs(movement.transform.position.x-ai.destination.x)>5f;
         
    }
   public void OnEnter()
   {
       startTime=Time.time;
       Debug.Log("Entered TargetDetectedState");       
       stateMachine.SetState(shoot);
   }
   
   public void OnExit()
   {
     
   }

   public void Tick()
   {
       stateMachine.Tick();
   }

}
