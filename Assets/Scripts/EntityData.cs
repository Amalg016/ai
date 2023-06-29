using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="entity",menuName ="Data/Entities")]
public class EntityData : ScriptableObject
{
    public int maxHp;
    public float speed;
    public float chargeSpeed;
    public float dodgeSpeed;
    public float range;
    public float shootInterval;

}
