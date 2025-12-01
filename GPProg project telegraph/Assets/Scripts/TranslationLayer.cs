using UnityEngine;
using UnityEngine.UIElements;

public class TranslationLayer : MonoBehaviour
{
    [SerializeField]
    ArmController armController;
    // Uses arm order left, Middle, Right
    private float[] armAngles = { 0.0f, 0.0f, 0.0f };
    private int[,] characterAngles = { { 180, 0, 0 },{ 180, 45, 0 },{ 180, 90, 0 },{ 180, 135, 0 },{ 0, 0, 180 },{ 0, 45, 180 },{ 0, 90, 180 }, { 0, 135, 180 },{ 0, 0, 0 }, { 0, 45, 0 }, { 0, 90, 0 }, { 0, 135, 0 }, { 180, 0, 180 }, 
        { 180, 45, 180 }, { 180, 90, 180 }, { 180, 135, 180 }, { 180, 0, 90 }, { 180, 45, 90 }, { 180, 90, 90 }, { 180, 135, 90 }, { 90, 0, 180 }, { 90, 45, 180 }, { 90, 90, 180 }, { 90, 135, 180 }, { 90, 0, 0 }, { 90, 45, 0 }, 
        { 90, 90, 0 }, { 90, 135, 0 }, { 0, 0, 90 }, { 0, 45, 90 }, { 0, 90, 90 }, { 0, 135, 90 }, { 90, 0, 90 }, { 90, 45, 90 }, { 90, 90, 90 }, { 90, 135, 90 } };
    private string characterOrder = "CBAHGFEDLKIMPONQTSRYXWVU1&Z654329870";
    void Start()
    {
        for (int i = 0; i < characterOrder.Length; i++)
        {
            int[] angles = { characterAngles[i,0], characterAngles[i,1], characterAngles[i,2] };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateArmAngles(float[] newAngles)
    {
        armAngles = newAngles;
        Debug.Log($"Arm angles updated - {armAngles}");
        armController.rotateTo((int)armAngles[1], (int)armAngles[2], (int)armAngles[0]);
    }

    public void updateSingleAngle(float angle, string arm)
    {
        float[] newAngles = armAngles;
        switch (arm)
        {
            case "Main":
                newAngles = new float[] { armAngles[0], angle, armAngles[2] };
                break;

            case "Left":
                newAngles = new float[] { angle, armAngles[1], armAngles[2] };
                break;

            case "Right":
                newAngles = new float[] { armAngles[0], armAngles[1], angle };
                break;

            default:
                print("Please use Main Left or Right keywords when selecting arm in updateSingleAngle");
                break;
        }
        armAngles = newAngles;
        print("["+string.Join(", ", armAngles) +"]");
        armController.rotateTo((int)armAngles[1], (int)armAngles[2], (int)armAngles[0]);
    }
}
