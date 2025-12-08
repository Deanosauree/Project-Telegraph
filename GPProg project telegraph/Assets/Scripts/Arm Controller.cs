using NUnit.Framework.Internal;
using Unity.Mathematics;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] private int[] desiredAngles = {0,0,0 };
    [SerializeField] private bool angleAchieved = false;
    [SerializeField] private bool[] correctAngles = {false,false,false};
    [SerializeField]
    [Header("Transforms for beams in order Big, Left, Right")]
    private Transform[] transforms;
    private int[] initialAngles = { 0,0,0};
    private float[] currentAngles = { 0,0,0};
    private const float rotationSpeed = 0.7f;

    private void Start()
    {

        initialAngles = new int[] { (int)transforms[0].localRotation.eulerAngles.y, (int)transforms[1].localRotation.eulerAngles.y, (int)transforms[2].localRotation.eulerAngles.y };
    }

    void FixedUpdate()
    {
        if (!angleAchieved)
        {
            Debug.Log("\nsettingAngle on "+ this.gameObject);

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
            angle = currentAngles[i];// * (180/math.PI);
            tr = transforms[i];
            float thisRotation = 0f;
            if (!correctAngles[i])
            {
                int desired = (desiredAngles[i]);// + initialAngles[i]);
                /*
                print("Desired:"+desired);
                print("Angle:"+angle);
                */
                //desiredRotation.eulerAngles.Set(desiredRotation.x,desiredRotation.y,desired);
                //Debug.Log($"Angle of {hingeJoints[i]} = {angle} and difference = {(float)desired - angle}");

                switch (desired - angle) 
                {
                    case (> rotationSpeed and < (180f)) or (<(-180f) and >(-360f+rotationSpeed)):
                        thisRotation = rotationSpeed;
                        tr.Rotate(0, thisRotation, 0);
                        break;
                    case (< (-rotationSpeed) and >= (-180f) or (>= (180f) and < (360f-rotationSpeed))):
                        thisRotation = -rotationSpeed; 
                        tr.Rotate(0, thisRotation, 0);
                        break;
                    case (<= rotationSpeed and >= -rotationSpeed) or (>= (360f-rotationSpeed) or <= (-360f+rotationSpeed)):
                        thisRotation = desired-angle;
                        tr.Rotate(0, thisRotation, 0);
                        correctAngles[i] = true;
                        numberOfCorrect++;
                        break;
                }
                
                currentAngles[i] = currentAngles[i] + thisRotation;
                /*
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
                */

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
