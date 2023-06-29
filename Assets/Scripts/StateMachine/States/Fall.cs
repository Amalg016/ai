using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : IState
{
  AnimationHelper animHelper;
  Movement movement;
  AI ai;
  public float startTime{get; set;}
    public Fall(AI ai,Movement movement)
    {
        animHelper=ai.animationHelper;
        this.movement=movement;
        this.ai=ai;
    }
  public void OnEnter()
  {
    startTime=Time.time;
    Debug.Log("enterd FallState");
  }
  public void OnExit()
  {   
    Debug.Log("exited FallState");
   // Time.timeScale=0;
  }
  public void Tick()
  {
     
  }
}
