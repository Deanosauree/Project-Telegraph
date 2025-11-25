using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputAction jumpAction;
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction lookx;
    private InputAction looky;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {

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
        
    }
    private void jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumped");
    }

    private void interact(InputAction.CallbackContext context) 
    {
        Debug.Log("Interacted");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        if (movement != Vector2.zero)
        {
            Debug.Log(movement);
        }
    }
}
