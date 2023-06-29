using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockable 
{
    public void knockback(Vector2 angle,float velocity,int direction,float xPos);
}
