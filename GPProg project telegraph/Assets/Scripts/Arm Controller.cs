using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] private int[] desiredAngles = {0,0,0 };
    [SerializeField] private bool angleAchieved = false;
    [SerializeField] private bool[] correctAngles = {false,false,false};
    private HingeJoint[] hingeJoints;
    private Rigidbody[] rigidBodies;

    private void Start()
    {
        // gets lists of the hinge joints and rigidbodies connected to the arms in the order {left, center, right}
        hingeJoints = new HingeJoint[] {
        transform.GetChild(0).GetChild(0).GetComponent<HingeJoint>(),
        GetComponentInChildren<HingeJoint>(),
        transform.GetChild(0).GetChild(1).GetComponent<HingeJoint>()
    };

        rigidBodies = new Rigidbody[] {
        transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>(),
        GetComponentInChildren<Rigidbody>(),
        transform.GetChild(0).GetChild(1).GetComponent<Rigidbody>()
        
    };
        Debug.Log(hingeJoints[0]);
    }

    void Update()
    {
        if (!angleAchieved)
        {
            //Debug.Log("settingAngle");
            setAngle();
        }
        
    }
    private void setAngle()
    {
        int numberOfCorrect = 0;
        for (int i = 0; i < 3; i++)
        {
            float angle;
            Rigidbody rb;
            angle = hingeJoints[i].angle+180;
            rb = rigidBodies[i];
            if (!correctAngles[i])
            {
                
                int desired = desiredAngles[i]+180;
                var motor = hingeJoints[i].motor;
                var spring = hingeJoints[i].spring;
                hingeJoints[i].useSpring = false;
                hingeJoints[i].useMotor = true;
                //Debug.Log($"Angle of {hingeJoints[i]} = {angle} and difference = {(float)desired - angle}");
                switch ((float)desired - angle)
                {
                    case (> (0.5f) and <(180f)) or (<(-180f) and >(-359.5f)):
                        //apply force clockwise
                        //float turn = Input.GetAxis("Horizontal");
                        motor.targetVelocity = 50;
                        break;
                    case (< (-0.5f) and >(-180f) or (>(180f) and <(359.5f))):
                        //apply force anticlockwise
                        motor.targetVelocity = -50;
                        break;
                    case (< (0.5f) and > (-0.5f)) or (>(359.5f)  or <(-359.5f)) :
                        //if angle is correct, set to true
                        motor.targetVelocity = 0;
                        spring.targetPosition = desired - 180;
                        hingeJoints[i].useSpring = true;
                        hingeJoints[i].useMotor = false;
                        Debug.Log($"Angle of {hingeJoints[i]} is correct because {(float)desired} < 5 and > -5");
                        correctAngles[i] = true;
                        numberOfCorrect++;
                        break;
                }
                hingeJoints[i].motor = motor;
                hingeJoints[i].spring = spring;
            }
            else
            {
                numberOfCorrect++;
            }
            if (numberOfCorrect == 3) 
            {
                Debug.Log("ts Working");
                angleAchieved = true;
            }



        }

        
    }

}
