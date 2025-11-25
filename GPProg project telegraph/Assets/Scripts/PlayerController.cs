using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float gravityValue = -9.8f;
    [SerializeField] float mouseSensitivity = 1f;
    [SerializeField] Camera playerCam;

    private Vector3 playerVelocity = Vector3.zero;

    private PlayerInput playerInput;
    private InputAction jumpAction;
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction lookx;
    private InputAction looky;
    private float camYRotation = 0f;
    private float rotate;



    private CharacterController characterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
        jumpAction.started += jump;

        interactAction = playerInput.actions["Interact"];
        interactAction.started += interact;

        moveAction = playerInput.actions["Move"];

        lookx = playerInput.actions["LookX"];
        looky = playerInput.actions["LookY"];
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void jump(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Vector3 p1 = transform.position + characterController.center;
        if (Physics.SphereCast(p1, characterController.radius, -transform.up, out hit, 1))
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -1.0f * gravityValue);
        }
        
    }

    private void interact(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Vector3 p1 = playerCam.transform.position;
        if (Physics.SphereCast(p1, characterController.radius * 0.2f, playerCam.transform.forward, out hit, 5))
        {
            Debug.DrawRay(p1, playerCam.transform.forward * 10, Color.green, 1, true);
            Debug.Log(hit.textureCoord);
        }
    }
    // Update is called once per frame
    void Update()
    {
        

        if (characterController.isGrounded & playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        Vector2 movement = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3((float)movement.x, 0.0f, (float)movement.y);
        move = Vector3.ClampMagnitude(move, 1.0f);

        playerVelocity.y += gravityValue*Time.deltaTime;
        Vector3 finalMove = new Vector3(move.x * movementSpeed, playerVelocity.y, move.z * movementSpeed);
        finalMove = transform.TransformDirection(finalMove);

        //camera movement 
        rotate = lookx.ReadValue<float>() * mouseSensitivity * Time.fixedDeltaTime;
        camYRotation -= looky.ReadValue<float>() * mouseSensitivity * Time.fixedDeltaTime;
        camYRotation = Mathf.Clamp(camYRotation, -90f, 90f);
        playerCam.transform.localRotation = Quaternion.Euler(camYRotation, 0, 0);

        characterController.Move(finalMove*Time.deltaTime);
        transform.Rotate(Vector3.up,rotate);
    }

}
