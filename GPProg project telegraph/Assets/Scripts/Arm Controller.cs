using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] private int[] desiredAngles = {0,0,0 };
    [SerializeField] private bool angleAchieved = false;
    [SerializeField] private bool[] correctAngles = {false,false,false};
    [SerializeField]
    private Transform[] transforms;
    private int[] initialAngles = { 0,0,0};
    private float rotationSpeed = 0.1f;

    private void Start()
    {

        initialAngles = new int[] { (int)transforms[0].localRotation.eulerAngles.z, (int)transforms[1].localRotation.eulerAngles.z, (int)transforms[2].localRotation.eulerAngles.z };
    }

    void FixedUpdate()
    {
        if (!angleAchieved)
        {
            Debug.Log("settingAngle");

            setAngle();
        }
        else
        {
            print("Disabled");
            enabled = false;
        }
        
    }

    public void rotateTo(int bigBeamAngle,int leftBeamAngle, int rightBeamAngle)
    {
        desiredAngles[1] = leftBeamAngle; desiredAngles[0] = bigBeamAngle; desiredAngles[2] = rightBeamAngle;
        correctAngles[0] = false; correctAngles[1] = false; correctAngles[2] = false;
        angleAchieved = false;
        enabled = true;
    }
    private void setAngle()
    {
        int numberOfCorrect = 0;
        for (int i = 0; i < 3; i++)
        {
            float angle;
            Transform tr;
            angle = transforms[i].localRotation.eulerAngles.z;
            tr = transforms[i];
            if (!correctAngles[i])
            {
                int desired = (-desiredAngles[i] + initialAngles[i]);
                print(desired);
                Quaternion currentRotation = tr.localRotation;
                Quaternion desiredRotation = Quaternion.Euler(currentRotation.eulerAngles.x,currentRotation.eulerAngles.y,desired);
                print(Quaternion.Angle(currentRotation, desiredRotation));
                print($"Supposedly my quaternions are {currentRotation.eulerAngles.y} and {desiredRotation.eulerAngles.y}");
                //desiredRotation.eulerAngles.Set(desiredRotation.x,desiredRotation.y,desired);
                //Debug.Log($"Angle of {hingeJoints[i]} = {angle} and difference = {(float)desired - angle}");
                if (!(Quaternion.Angle(currentRotation, desiredRotation) < 0.001))
                {
                    print($"{i} Slerping from {currentRotation.eulerAngles.z} to {desiredRotation.eulerAngles.z} and I got these values from {tr}");
                    tr.localRotation = Quaternion.Slerp(currentRotation, desiredRotation, rotationSpeed);
                }
                else 
                {
                    print($"This arm is correct because {desiredRotation.eulerAngles.z} == {currentRotation.eulerAngles.z}");
                    correctAngles[i] = true;
                }

            }
            else
            {
                numberOfCorrect++;
                
            }
            if (numberOfCorrect == 3) 
            {
                print(string.Join(", ",desiredAngles));
                angleAchieved = true;
            }



        }

        
    }

}
