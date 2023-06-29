using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Roam : IState
{
   AI ai;
   Movement movement;
   AnimationHelper animationHelper;
   public float startTime{get; set;}
   public Roam(AI ai,Movement movement)
   {
      this.ai=ai;
      this.movement=movement;   
      animationHelper=ai.animationHelper;
   }
    public void OnEnter(){
        startTime=Time.time;
        Debug.Log("Entered RoamState") ; 
        animationHelper.Move();
    }
    public void OnExit(){
        Debug.Log("Exited RoamState");
    }

    public void Tick()
    {
        movement.SetVelocityX(ai.entityData.speed*movement.FacingDirection);             
        var col=Physics2D.Raycast(movement.WallCheck.position,movement.transform.right,ai.entityData.range,ai.playerLayerMask);
         if(col)
         { 
              ai.target= col.collider.gameObject.transform;         
         }
    }
}
