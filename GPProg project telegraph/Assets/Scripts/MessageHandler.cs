using JetBrains.Annotations;
using System;
using Unity.Profiling.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    TranslationLayer sendingTower;
    [SerializeField]
    TranslationLayer playerTower;
    [SerializeField]
    TranslationLayer recievingTower;
    [SerializeField]
    int startingSecondsToConfirrm = 0;

    private int thisCharFor = 0;

    private bool sendingMessage = false;

    private string currentMessage = "";
    private int currentCharacter = 0;
    private int totalMistakes = 0;
    private float averageCharacterTime = 0;
    private int lastMessageMistakes = 0;
    private float lastMessageTime = 0;
    private int currentMessageMistakes = 0;
    private float currentMessageTime = 0;
    private float charHoldSeconds;

    private char currentPlayerLetter = '~';
    private char lastPlayerLetter = '~';

    private void Awake()
    {
        charHoldSeconds = startingSecondsToConfirrm;
    }
    private void Start()
    {
        lastPlayerLetter = playerTower.getLetter();
        startNewMessage("FRANCE");

    }
    private void startNewMessage(string message)
    {
        if (sendingMessage) 
        {
            if (averageCharacterTime != 0)
            {
                averageCharacterTime = (averageCharacterTime + (currentMessageTime / currentCharacter));
            }
            else { averageCharacterTime = (currentMessageTime / currentCharacter); }
            
        }
        print(message);

        lastPlayerLetter = playerTower.getLetter();

        sendingMessage = true;
        currentMessage = message;
        currentCharacter = 0;
        lastMessageMistakes = currentMessageMistakes;
        lastMessageTime = currentMessageTime;
        currentMessageTime = 0;
        currentMessageMistakes = 0;
        InvokeRepeating("runMessageCheck", 0, 1);
        sendNewCharacter(currentMessage[0], sendingTower);

    }

    private void sendNewCharacter(char character,TranslationLayer tower)
    {
        tower.setLetter(character);
    }


    private void runMessageCheck()
    {
        if (sendingMessage)
        {
            char currentLetter = currentMessage[currentCharacter];
            char playerLetter = playerTower.getLetter();
            currentMessageTime++;
            if (currentPlayerLetter == playerLetter)
            {
                thisCharFor++;
            }
            else { thisCharFor = 0; lastPlayerLetter = currentPlayerLetter; currentPlayerLetter = playerLetter; }
            if (thisCharFor > charHoldSeconds && currentPlayerLetter != lastPlayerLetter && lastPlayerLetter != '~')
            {
                print($"Last letter is {lastPlayerLetter} and this letter is {playerLetter}");
                print($"Letter sent: {playerLetter}\nCorrectLetter: {currentLetter}");
                lastPlayerLetter = currentPlayerLetter;
                if (currentPlayerLetter == currentLetter)
                {
                    sendNewCharacter(currentLetter, recievingTower);
                }
                else 
                {
                    sendNewCharacter(currentPlayerLetter, recievingTower);
                    totalMistakes++;
                    currentMessageMistakes++;
                }
                currentCharacter++;
                if (currentCharacter == currentMessage.Length)
                {
                    sendingMessage = false;
                    CancelInvoke();
                }
                else
                {
                    sendNewCharacter(currentMessage[currentCharacter], sendingTower);
                }
            }
           
        }

        
    }
}
