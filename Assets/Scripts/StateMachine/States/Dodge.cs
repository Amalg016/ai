using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : IState
{
  AnimationHelper animHelper;
  E1TargetDetected playerDetected;
  Movement movement;
  AI ai;
  public float startTime{get; set;}
   public Dodge(AI ai,Movement movement,E1TargetDetected playerDetected)
    {
        animHelper=ai.animationHelper;
        this.movement=movement;
        this.playerDetected=playerDetected;
        this.ai=ai;
    }
   public void OnEnter()
   {
       startTime=Time.time;
       ai.animationHelper?.Dodge();
       Debug.Log("Entered ShootState");  
       movement.SetVelocityX(0);
   }
   

   public void OnExit()
   {
    ai.animationHelper.SetDodgeF();
   }

   public void Tick()
   {
     movement.SetVelocity(ai.entityData.dodgeSpeed,new Vector2(1,1),-movement.FacingDirection);     
   }
}
