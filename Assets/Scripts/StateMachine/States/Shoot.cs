using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : IState
{
        AnimationHelper animHelper;
        E1TargetDetected playerDetected;
  Movement movement;
  AI ai;
  
public float startTime{get; set;}
   public Shoot(AI ai,Movement movement,E1TargetDetected playerDetected)
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
     //ai.timer=0;    
   }

   public void Tick()
   {
    if(ai.timer==0)
        animHelper?.Fire();

     ai.timer+=Time.deltaTime;
     
     if(ai.timer>ai.entityData.shootInterval){ 
        animHelper?.Fire();
        ai.timer=0;
     }
   }
}
