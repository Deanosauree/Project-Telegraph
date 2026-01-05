using UnityEngine;


public interface IState
{
    void Execute();
}
public abstract class State : IState
{
    protected StateMachine controller;

    protected MessageHandler messager;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public State(StateMachine controller, MessageHandler messager) 
    {
        this.controller = controller;
        this.messager = messager;
    }
    public abstract void Execute();
}
