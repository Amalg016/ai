using System;
using UnityEngine;

public class SoldierAI : AI
{
     FSM<SoldierAI> StateMachine;

   Roam search;
   ObstacleCheck idle;
   E2TargetDetected charge;
    // Start is called before the first frame update
    void Start()
    {
        StateMachine=new FSM<SoldierAI>(this);
        
         search =new Roam(this,movement);
         idle =new ObstacleCheck(this,movement);
         charge =new E2TargetDetected(this,movement);

        StateMachine.AddTransition(search,idle,shouldFlip());
        StateMachine.AddTransition(idle,search,()=>Time.time>idle.startTime+1);
        StateMachine.AddTransition(idle,charge,PlayerinRange());
        StateMachine.AddTransition(search,charge,PlayerinRange());
        StateMachine.AddTransition(charge,idle,PlayerNotinRange());
        
        StateMachine.SetState(search);

        Func<bool> shouldFlip()=>()=> Physics2D.Raycast(movement.WallCheck.position,transform.right,1,layerMask)||!Physics2D.Raycast(movement.GroundCheck.position,-transform.up,1,layerMask);
        Func<bool> PlayerinRange()=>()=>target;
        Func<bool> PlayerNotinRange()=>()=>!target;
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

    public override void Die()
    {
        Debug.Log("dead");
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
      if(charged)
      {
        Debug.Log("damage done");
        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(100);
      }

    }
    
    void SpearAttack()
    {
      var col=Physics2D.OverlapCircle(movement.playerCheck.position,.2f);  
     if(col){
      col.gameObject.GetComponent<IDamageable>()?.TakeDamage(20);
      col.gameObject.GetComponent<IKnockable>()?.knockback(new Vector2(1,1),5,movement.FacingDirection,transform.position.x);
     }
    }
}
