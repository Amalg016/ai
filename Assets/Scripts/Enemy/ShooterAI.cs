using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShooterAI : AI
{
   FSM<ShooterAI> StateMachine;
    
    // Start is called before the first frame update
    void Start()
    {
            StateMachine=new FSM<ShooterAI>(this);
        
        var search =new Roam(this,movement);
        var idle =new ObstacleCheck(this,movement);
        //var run=new Run(this,movement);
        var targetFound=new E1TargetDetected(this,movement);
        var fall =new Fall(this,movement);
        
        StateMachine.AddTransition(search,idle,shouldFlip());
        StateMachine.AddTransition(idle,search,()=>Time.time>idle.startTime+1);
       // StateMachine.AddTransition(idle,fear,PlayerinRange());//
        StateMachine.AddTransition(search,targetFound,PlayerinRange());
         StateMachine.AddTransition(targetFound,search,PlayerNotinRange());
        
        StateMachine.AddAnyTransition(fall,ShouldFall());
        StateMachine.AddTransition(fall,search,isGrounded());
       // StateMachine.AddTransition(fall,search,isGrounded());

       // StateMachine.AddTransition(run,search,PlayerNotinRange());
        
        StateMachine.SetState(search);

        Func<bool> shouldFlip()=>()=> Physics2D.Raycast(movement.WallCheck.position,transform.right,1,layerMask)||!Physics2D.Raycast(movement.GroundCheck.position,-transform.up,.3f,layerMask);
        Func<bool> PlayerinRange()=>()=>target;
        Func<bool> ShouldFall()=>()=>!Grounded;
        Func<bool> isGrounded()=>()=>Grounded;
      //  Func<bool> PlayerNotinRange()=>()=>run.stop;
       Func<bool> PlayerNotinRange()=>()=>!target;
    }

    // Update is called once per frame
     public override void Update()
    {
        base.Update();
          StateMachine.Tick();      
    }   
}
