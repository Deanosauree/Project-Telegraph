using UnityEngine;
using System.IO;

public class SafeState : State
{

    
    public SafeState(StateMachine controller, MessageHandler messager) : base(controller, messager)
    {
        // sets up initial state
        minWaitTime = 20;
        maxWaitTime = 50;
        filePath = "Assets/StateText/SafeText.txt";
        if (File.Exists(filePath))
        {
            messages = File.ReadAllLines(filePath);
        }
        else
        {
            Debug.Log($"{filePath} Not Found");
        }
    }

    public override void Execute()
    {
        // Execute is what the state machine calls when requesting a new message
        int waitTime = Random.Range(minWaitTime, maxWaitTime);
        sendNewMessage(waitTime, messages[Random.Range(0,messages.Length)]);
    }

    public override void Initialise()
    {

    }
}
