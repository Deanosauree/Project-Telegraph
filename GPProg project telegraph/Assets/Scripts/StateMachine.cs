using NUnit.Framework;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private MessageHandler handler;
    private State currentState;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.Execute();
    }

    public void ChangeState(State newState)
    {
        currentState = newState;
    }
}
