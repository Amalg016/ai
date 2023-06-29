using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public float startTime{get;set;}
    public void OnEnter();
    public void OnExit();
    public void Tick();
}
