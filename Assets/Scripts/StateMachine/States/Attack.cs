using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState
{
      AnimationHelper animHelper;
        E2TargetDetected playerDetected;
  Movement movement;
  AI ai;
  
public float startTime{get; set;}
   public Attack(AI ai,Movement movement,E2TargetDetected playerDetected)
    {
        animHelper=ai.animationHelper;
        this.movement=movement;
        this.playerDetected=playerDetected;
        this.ai=ai;
    }
   public void OnEnter()
   {
       startTime=Time.time;
       Debug.Log("Entered ShootState");  
       movement.SetVelocityX(0);  
   }
   

   public void OnExit()
   {
           animHelper?.SetAttackF();
   }

   public void Tick()
   {
    if(ai.timer==0)
        animHelper?.Attack();

     ai.timer+=Time.deltaTime;
     
     if(ai.timer>ai.entityData.shootInterval){ 
             animHelper?.Attack();
        ai.timer=0;
     }
   }
}
