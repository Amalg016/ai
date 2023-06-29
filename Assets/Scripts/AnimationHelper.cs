using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHelper : MonoBehaviour
{
    Animator animator;
    [SerializeField] string attack;
    [SerializeField] public string move;
    [SerializeField] public string idle;
    [SerializeField] public string dodge;
    public void Awake(){
        animator=GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public void Move()
    {
     animator.SetBool(move,true);
     animator.SetBool(idle,false);
     animator.SetBool(attack,false);
    }

    public void Idle()
    {
     animator.SetBool(move,false);
      animator.SetBool(idle,true);
     animator.SetBool(attack,false);
    }

    public void Attack(){
     animator.SetBool(move,false);
     animator.SetBool(idle,false);
     animator.SetBool(attack,true);
    }
    public void Dodge(){
     animator.SetBool(move,false);
     animator.SetBool(idle,false);
     animator.SetBool(dodge,true);
    }
    public void Fire(){
     animator.SetBool(move,false);
     animator.SetBool(idle,false);
     animator.SetTrigger(attack);
    }

public void SetDodgeF(){
     animator.SetBool(dodge,false);
}
public void SetAttackF(){
     animator.SetBool(attack,false);
}
    public void ResetTrigger(){
        animator.ResetTrigger(attack);
    }    
}
