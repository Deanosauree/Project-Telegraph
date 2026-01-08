using UnityEngine;
using System.IO;
using Unity.VisualScripting;
public class LoomingState : State
{


    public LoomingState(StateMachine controller, MessageHandler messager) : base(controller, messager)
    {
        minWaitTime = 10;
        maxWaitTime = 60;
        filePath = "Assets/StateText/LoomingText.txt";
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
        sendNewMessage(waitTime, messages[Random.Range(0, messages.Length)]);
    }

    public override void Initialise()
    {

    }
}
