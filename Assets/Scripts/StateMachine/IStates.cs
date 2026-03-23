using UnityEngine;

public interface IStates 
{
    void OnEnter();
    void Update();
    void FixedUpdate();
    void OnExit();
}
