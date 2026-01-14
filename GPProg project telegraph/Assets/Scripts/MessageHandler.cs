using JetBrains.Annotations;
using System;
using System.Collections;
using Unity.Profiling.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MessageHandler : MonoBehaviour
{
    public UnityEvent messageComplete;
    public UnityEvent onMistake;

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

    public int getTotalMistakes()
    {
        return totalMistakes;
    }
    private void Awake()
    {
        charHoldSeconds = startingSecondsToConfirrm;
    }
    private void Start()
    {
        lastPlayerLetter = playerTower.getLetter();
        Invoke("CallForMessage",1f); // delays to ensure all required references exist

    }

    private void CallForMessage()
    {
        print("CallingMessage");
        messageComplete.Invoke();
    }

    public IEnumerator WaitToMessage(string message, int waitTime)
    {
        // allows the state to call for a new message on the frame it is asked but give a delay before the message starts
        print("Starting message in: " + waitTime);
        yield return new WaitForSeconds(waitTime);
        startNewMessage(message);
        StopAllCoroutines();
    }
    private void startNewMessage(string message)
    {
        // cancels current message and updates tracking values
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
        InvokeRepeating("runMessageCheck", 0, 1); // invokes runmessagecheck to run every second
        sendNewCharacter(currentMessage[0], sendingTower);

    }

    private void endOfMessage()
    {
        messageComplete.Invoke();
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
            if (currentPlayerLetter == playerLetter) // if the letter represented by the tower has not changed
            {
                thisCharFor++;
            }
            else { thisCharFor = 0; lastPlayerLetter = currentPlayerLetter; currentPlayerLetter = playerLetter; } // reset timer if the letter changes, set the last player letter
            if (thisCharFor > charHoldSeconds && currentPlayerLetter != lastPlayerLetter && lastPlayerLetter != '~') 
                // if the same letter has been held for a set time AND the letter has been changed at some point since the last character
            {
                print($"Letter sent: {playerLetter}\nCorrectLetter: {currentLetter}");
                lastPlayerLetter = currentPlayerLetter;
                if (currentPlayerLetter == currentLetter) // check if the player got the character correct
                {
                    sendNewCharacter(currentLetter, recievingTower); // tells next tower in chain to copy the player
                }
                else
                {
                    sendNewCharacter(currentPlayerLetter, recievingTower);
                    totalMistakes++;
                    currentMessageMistakes++;
                    onMistake.Invoke();
                }
                currentCharacter++;
                if (currentCharacter == currentMessage.Length) // checks if this character is the final of the message
                {
                    sendingMessage = false;
                    CancelInvoke();
                    endOfMessage();
                }
                else
                {
                    sendNewCharacter(currentMessage[currentCharacter], sendingTower); // sends a new character for the sending tower to adopt
                }
            }
           
        }

        
    }
}
