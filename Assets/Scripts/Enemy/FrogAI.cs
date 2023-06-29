using System;
using UnityEngine;

public class FrogAI : AI
{

    FSM<FrogAI> StateMachine;
    
    // Start is called before the first frame update
    void Start()
    {
            StateMachine=new FSM<FrogAI>(this);
        
        var search =new Roam(this,movement);
        var idle =new ObstacleCheck(this,movement);
        var run=new Run(this,movement);
        var fear=new Fear(this,movement);
        var fall =new Fall(this,movement);
        
        StateMachine.AddTransition(search,idle,shouldFlip());
        StateMachine.AddTransition(idle,search,()=>Time.time>idle.startTime+1);
       // StateMachine.AddTransition(idle,fear,PlayerinRange());
        StateMachine.AddTransition(search,fear,PlayerinRange());
        StateMachine.AddTransition(fear,run,CanStopFear());
        
        StateMachine.AddAnyTransition(fall,ShouldFall());
        StateMachine.AddTransition(fall,run,isGrounded());
       // StateMachine.AddTransition(fall,search,isGrounded());

        StateMachine.AddTransition(run,search,PlayerNotinRange());
        
        StateMachine.SetState(search);

        Func<bool> shouldFlip()=>()=> Physics2D.Raycast(movement.WallCheck.position,transform.right,1,layerMask)||!Physics2D.Raycast(movement.GroundCheck.position,-transform.up,.3f,layerMask);
        Func<bool> PlayerinRange()=>()=>Physics2D.Raycast(movement.WallCheck.position,transform.right,5,playerLayerMask);
        Func<bool> ShouldFall()=>()=>!Grounded;
        Func<bool> isGrounded()=>()=>Grounded;
        Func<bool> PlayerNotinRange()=>()=>run.stop;
        Func<bool> CanStopFear()=>()=> Time.time>fear.startTime+1;
    }

    // Update is called once per frame
     public override void Update()
    {
        base.Update();
        StateMachine.Tick();      
        if(Grounded&&knockbacked&&rb.velocity.y<=0.01){
              movement.CanSetVelocity=true;
              knockbacked=false;
        }
        if(Grounded&&rb.velocity.y!=0)
        {
              movement.SetVelocityY(0);
        }
    }      
    public override void knockback(Vector2 angle,float velocity,int direction,float xPos)
    {
        if(xPos<transform.position.x&& movement.FacingDirection==-1)
        {
             movement.Flip();
        }
        else if(xPos>transform.position.x&& movement.FacingDirection==1)
        {
             movement.Flip();
        }
        if(charged){
        movement.SetVelocity(velocity+3,angle,direction);
        }
        else{
        movement.SetVelocity(velocity,angle,direction);
        }
        movement.CanSetVelocity=false;
        knockbacked=true;
    }
}
