using UnityEngine;
using System.IO;

public class SafeState : State
{

    
    public SafeState(StateMachine controller, MessageHandler messager) : base(controller, messager)
    {
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
        int waitTime = Random.Range(minWaitTime, maxWaitTime);
        sendNewMessage(waitTime, messages[Random.Range(0,messages.Length)]);
    }

    public override void Initialise()
    {

    }
}
