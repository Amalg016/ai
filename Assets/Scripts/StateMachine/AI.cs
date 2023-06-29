using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour,IDamageable,IKnockable
{
    public Rigidbody2D rb{get;private set;}
    protected Movement movement;
    public AnimationHelper animationHelper;
    public bool charged=false;
    public LayerMask playerLayerMask;
    public LayerMask layerMask;
    [SerializeField]protected int currentHp;  
    public bool Grounded=false;
    [SerializeField]Transform shootPoint;
    [SerializeField]GameObject shootFx;
    public EntityData entityData;

    [SerializeField]protected bool knockbacked=false;
   public Transform target;
   public Vector2 destination;    
   public float timer=0;
    void Awake()
    {
        currentHp=entityData.maxHp;
        this.rb=GetComponent<Rigidbody2D>();
        this.movement=GetComponent<Movement>();
        animationHelper=GetComponent<AnimationHelper>();
    }
    public void TakeDamage(int damage)
    {    
       currentHp-=damage;
      // Debug.Log("died");
       if(currentHp<=0){
          Die();
       }
    }
    public virtual void knockback(Vector2 angle,float velocity,int direction,float xPos)
    {
        if(xPos<transform.position.x&& movement.FacingDirection==1)
        {
             movement.Flip();
        }
        else if(xPos>transform.position.x&& movement.FacingDirection==-1)
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
    
    public void Shoot()
    {
      GameObject ob=GameObject.Instantiate(shootFx,shootPoint.position,Quaternion.identity);
      Destroy(ob,.2f);
      var col=Physics2D.Raycast(shootPoint.position,transform.right,5); 
      if(col){
      col.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(25); 
      col.collider.gameObject.GetComponent<IKnockable>()?.knockback(new Vector2(1,1f),5,movement.FacingDirection,transform.position.x); 
      }
      animationHelper?.ResetTrigger();
    }
    public virtual void Die()
    {
      Destroy(this.gameObject);
    }   

    public virtual void Update()
    {
             if(Physics2D.Raycast(movement.FallCheck.position,-transform.up,.1f,layerMask)){
            Grounded=true;
        }
        else{Grounded=false;}
    if(target){destination=target.transform.position;}
    }
}
