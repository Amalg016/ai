using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fear : IState
{ AnimationHelper animHelper;
  Movement movement;
  AI ai;

public float startTime{get; set;}
    public Fear(AI ai,Movement movement)
    {
        animHelper=ai.animationHelper;
        this.movement=movement;
        this.ai=ai;
    }
   public void OnEnter()
   {
       startTime=Time.time;
       Debug.Log("Entered FearState"); 
       animHelper?.Attack();   
   }
   
   public void OnExit()
   {
    movement.Flip();
   }

   public void Tick()
   {
      movement.SetVelocityY(.1f);
   }


}
