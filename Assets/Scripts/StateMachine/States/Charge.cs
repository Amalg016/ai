using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : IState
{
  AnimationHelper animHelper;
  Movement movement;
  AI ai;
  public bool stop=false;
  public float timer;
  public bool StartTimer=false;
  public float startTime{get; set;}

    public Charge(AI ai,Movement movement)
    {
        animHelper=ai.animationHelper;
        this.movement=movement;
        this.ai=ai;
    }
   public void OnEnter()
   {
       startTime=Time.time;
       Debug.Log("Entered ChargeState")  ; 
       animHelper?.Move();
       ai.charged=true;    
   }

   public void check(){
    if(!ai.target){
      StartTimer=true;
    }
    else{StartTimer=false;timer=0;}
   }
   public void OnExit()
   {
    ai.charged=false;
    timer=0;
    StartTimer=false;
    stop=false;
   }
   public void Tick() 
   {
      check();
      if(StartTimer){
          timer+=Time.deltaTime;
      }
      
      movement.SetVelocityX(ai.entityData.chargeSpeed*movement.FacingDirection);
      
      if(timer>.3f)stop=true;
   }
}
