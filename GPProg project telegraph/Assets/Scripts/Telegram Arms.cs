using UnityEngine;
using UnityEngine.UIElements;

public class TelegramArms : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float[] armAngles = {0.0f,0.0f,0.0f};
    [SerializeField] private int[] correctedAndgles = {0,0,0};
    private int[,] characterAngles = { { 180, 0, 0 },{ 180, 45, 0 },{ 180, 90, 0 },{ 180, 135, 0 },{ 0, 0, 180 },{ 0, 45, 180 },{ 0, 90, 180 }, { 0, 135, 180 },{ 0, 0, 0 }, { 0, 45, 0 }, { 0, 90, 0 }, { 0, 135, 0 }, { 180, 0, 180 }, 
        { 180, 45, 180 }, { 180, 90, 180 }, { 180, 135, 180 }, { 180, 0, 90 }, { 180, 45, 90 }, { 180, 90, 90 }, { 180, 135, 90 }, { 90, 0, 180 }, { 90, 45, 180 }, { 90, 90, 180 }, { 90, 135, 180 }, { 90, 0, 0 }, { 90, 45, 0 }, 
        { 90, 90, 0 }, { 90, 135, 0 }, { 0, 0, 90 }, { 0, 45, 90 }, { 0, 90, 90 }, { 0, 135, 90 }, { 90, 0, 90 }, { 90, 45, 90 }, { 90, 90, 90 }, { 90, 135, 90 } };
    private string characterOrder = "CBAHGFEDLKIMPONQTSRYXWVU1&Z654329870";
    void Start()
    {
        for (int i = 0; i < characterOrder.Length; i++)
        {
            int[] angles = { characterAngles[i,0], characterAngles[i,1], characterAngles[i,2] };
            Debug.Log($"{characterOrder[i]} - [{angles[0]}, {angles[1]}, {angles[2]}]");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateArmAngles(float[] newAngles)
    {
        armAngles = newAngles;
        Debug.Log($"Arm angles updated - {armAngles}");
    }
}
