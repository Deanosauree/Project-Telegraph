using UnityEngine;

public class armInteractable : MonoBehaviour, Interactable
{
    enum armOptions {Center,Left,Right};
    [SerializeField]
    armOptions selectedArm = armOptions.Center;
    [SerializeField]
    TranslationLayer translationLayer;
    Transform armTransform;
    private int[] mainAngles = { 0, 45, 90, 135 };
    private int[] secondaryAngles = { 0, 90, 180 };
    private int angleSelected = 0;
    private int inititalAngle;

    void Awake()
    {
        armTransform = GetComponent<Transform>();
        inititalAngle = (int)transform.rotation.z;
    }
    public void Interact(Transform interactorTransform) // rotates though the relevant list of angles depending on arm type
    {
        int rotation = 0;
        switch (selectedArm) 
        { 
            case armOptions.Center:
                if (angleSelected < 3)
                {
                    angleSelected++;
                    rotation = -45;
                }
                else
                {
                    angleSelected = 0;
                    rotation = 135;
                }
                translationLayer.updateSingleAngle(mainAngles[angleSelected], "Main");
                transform.Rotate(new Vector3(0, 0, rotation));
                break;

            case armOptions.Left:
                if (angleSelected < 2)
                {
                    angleSelected++;
                    rotation = -90;
                }
                else
                {
                    angleSelected = 0;
                    rotation = 180;
                }
                translationLayer.updateSingleAngle(secondaryAngles[angleSelected], "Left");
                transform.Rotate(new Vector3(0, 0, rotation));
                break;

            case armOptions.Right:
                if (angleSelected < 2)
                {
                    angleSelected++;
                    rotation = -90;
                }
                else
                {
                    angleSelected = 0;
                    rotation = 180;
                }
                translationLayer.updateSingleAngle(secondaryAngles[angleSelected], "Right");
                transform.Rotate(new Vector3(0, 0, rotation));
                break;

        }
        transform.Rotate(new Vector3(0, 0, 0));
    }

}
