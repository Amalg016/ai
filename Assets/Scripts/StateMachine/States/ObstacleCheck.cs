using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheck :IState
{
   AnimationHelper animHelper;
   Movement movement;
   AI ai;
   public float startTime{get; set;}
   public ObstacleCheck(AI ai,Movement movement)
   {
        animHelper=ai.animationHelper;
        this.movement=movement;
        this.ai=ai;
   }
   public void OnEnter()
   {    
      startTime=Time.time;
      Debug.Log("Entered IdleState")  ; 
     movement.SetVelocityX(0);
     animHelper.Idle();
   }
   public void OnExit()
   {
      movement.Flip();      
   }
   public void Tick()
   {
   }
  
}
