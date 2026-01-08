using UnityEngine;
using System.IO;
using Unity.VisualScripting.FullSerializer;

public class UncertainState : State
{


    public UncertainState(StateMachine controller, MessageHandler messager) : base(controller, messager)
    {
        minWaitTime = 15;
        maxWaitTime = 30;
        filePath = "Assets/StateText/UncertainText.txt";
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
        Debug.Log("Ececute Uncertain State");
    }


    public override void Initialise()
    {
        Debug.Log("Initialise UncertainState");
    }
}
