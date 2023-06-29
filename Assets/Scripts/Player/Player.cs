using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    int currentHp;
    [SerializeField]int maxHp=100;
    // Start is called before the first frame update
    void Start()
    {
        currentHp=maxHp;
    }

    // Update is called once per frame
    void Update()
    {
         //Input movement etc; 
    }

    public void TakeDamage(int damage)
    {
         currentHp-=damage;
         if(currentHp<=0){
            Die();
         }
    }

    void Die(){
        Destroy(this.gameObject);
    }
}
