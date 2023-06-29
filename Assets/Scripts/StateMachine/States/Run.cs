using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : IState
{
 AnimationHelper animHelper;
  Movement movement;
  AI ai;
  public bool stop=false;
  public float startTime{get; set;}

    public Run(AI ai,Movement movement)
    {
        animHelper=ai.animationHelper;
        this.movement=movement;
        this.ai=ai;
    }
   public void OnEnter()
   {
    startTime=Time.time;
       Debug.Log("Entered RunState"); 
       movement.SetVelocityY(0);
       animHelper?.Move();          
   }

   public void check(){
   }
   public void OnExit()
   {
    stop=false;
   }
   public void Tick() 
   {
      movement.SetVelocityX(ai.entityData.chargeSpeed*movement.FacingDirection);
      if(Time.time>startTime+5)stop=true;
   }
}
