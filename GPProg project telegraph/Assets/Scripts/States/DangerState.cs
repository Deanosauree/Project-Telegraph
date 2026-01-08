using UnityEngine;
using System.IO;
public class DangerState : State
{


    public DangerState(StateMachine controller, MessageHandler messager) : base(controller, messager)
    {
        minWaitTime = 10;
        maxWaitTime = 15;
        filePath = "Assets/StateText/DangerText.txt";
        if (File.Exists(filePath))
        {
            messages = File.ReadAllLines(filePath);
        }
        else
        {
            Debug.Log($"{filePath} Not Found");
        }
    }

    public override void Initialise()
    {
        ExecuteImmidiately();
    }
    public override void Execute()
    {
        int waitTime = Random.Range(minWaitTime, maxWaitTime);
        sendNewMessage(waitTime, messages[Random.Range(0, messages.Length)]);
    }

    private void ExecuteImmidiately()
    {
        sendNewMessage(0, messages[Random.Range(0, messages.Length)]);
    }

}
