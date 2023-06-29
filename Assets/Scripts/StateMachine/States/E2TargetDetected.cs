using System;
using UnityEngine;

public class E2TargetDetected : IState
{
    
    AnimationHelper animHelper;
  Movement movement;
  FSM<E2TargetDetected> stateMachine;
  AI ai;
  Run chase;
  Attack attack;
    public float startTime{get; set;}
    public E2TargetDetected(AI ai,Movement movement)
    {
        animHelper=ai.animationHelper;
        this.movement=movement;
        this.ai=ai;
        stateMachine=new FSM<E2TargetDetected>(this);
         attack=new Attack(ai,movement,this);
         chase=new Run(ai,movement);
         stateMachine.AddTransition(attack,chase,canChase());
         stateMachine.AddTransition(chase,attack,canShoot());
       //  Func<bool> canDodge()=>()=> Physics2D.OverlapCircle(movement.playerCheck.transform.position,.1f,ai.playerLayerMask);
         Func<bool> canShoot()=>()=> ai.Grounded &&ai.target&&Mathf.Abs(movement.transform.position.x-ai.destination.x)<.4f;
         Func<bool> canChase()=>()=> Mathf.Abs(movement.transform.position.x-ai.destination.x)>0.4f;
                
    }
 public void OnEnter(){
   startTime=Time.time;
       Debug.Log("Entered TargetDetectedState");       
       stateMachine.SetState(chase); 
 }
 public void OnExit(){

 }
 public void Tick()
 {
stateMachine.Tick();
 }
 }
