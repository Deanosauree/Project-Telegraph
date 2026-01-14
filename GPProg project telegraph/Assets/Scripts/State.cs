using UnityEngine;
using System.IO;
using System.Collections;


public interface IState
{
    void Execute();
    void cancel();
}
public abstract class State : IState
{
    protected StateMachine controller;

    protected MessageHandler messager;

    protected int minWaitTime;

    protected int maxWaitTime;

    protected string filePath;

    protected string[] messages;

    protected IEnumerator coroutine;


    public State(StateMachine controller, MessageHandler messager) 
    {
        this.controller = controller;
        this.messager = messager;
    }
    public abstract void Execute();

    public abstract void Initialise();

    public void sendNewMessage(int waitTime, string message)
    {
        coroutine = messager.WaitToMessage(message, waitTime);
        messager.StartCoroutine(coroutine);
    }
    public void cancel()
    {
        if (coroutine != null)
        {
            messager.StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
