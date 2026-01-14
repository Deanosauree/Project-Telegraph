using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField]
    private int[] mistakeValues;
    private MessageHandler handler;
    private State currentState;
    private int stateNum;
    private bool messaging = true;


    void Awake()
    {
        handler = GetComponent<MessageHandler>();
        
    }
    private void Start()
    {
        ChangeState(new SafeState(this, handler));
    }

    // Update is called once per frame

    private void setMessaging(bool setMessaging)
    {
        messaging = setMessaging;
    }

    public void ChangeState(State newState) // creates the next state and switches
    {
        if (currentState != null)
        {
            currentState.cancel();
        }
        currentState = newState;
        print("Set state to " + newState);
        currentState.Initialise();
    }

    public void startNewMessage() // tells the current state to excecute
    {
        if (messaging)
        {
            print("NewMessageRequest Recieved");
            currentState.Execute();
        }
    }

    public void wrongCharacter() // switches state if enough errors have been made
    {
        int totalWrong = handler.getTotalMistakes();
        print("wrong character, total wrong charactes is " + totalWrong + "\nrequired for next state is "+ mistakeValues[stateNum]);
        if ((mistakeValues[stateNum] == totalWrong) & stateNum < mistakeValues.Length)   
        {
            switch (stateNum)
            {
                case 0:
                    stateNum++;
                    ChangeState(new UncertainState(this, handler));
                    break;

                case 1:
                    stateNum++;
                    ChangeState(new LoomingState(this, handler));
                    break;

                case 2:
                    stateNum++;
                    ChangeState(new DangerState(this, handler));
                    break;
            }
        }
    }
}

