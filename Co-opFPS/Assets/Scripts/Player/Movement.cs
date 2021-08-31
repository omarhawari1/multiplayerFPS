using UnityEngine;
using Mirror;


public class Movement : NetworkBehaviour
{
    [Header("Movement:")]
    [SerializeField]private float speed = 10f;
    [SerializeField]private float sprintSpeed = 15f;
    [SerializeField]private float mouseSensX = 100f;
    [SerializeField]private float mouseSensY = 100f;
    [SerializeField]private float gravity = -9.81f;
    [SerializeField]private float jumpHeight = 3f;
    [SerializeField]private Transform groundCheck;
    [SerializeField]private LayerMask groundLayer;
    [Header("Setup:")]
    [SerializeField]private float cameraOffSetY;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Camera mainCamera;
    private float xRotation;

    public override void OnStartLocalPlayer()
    {
        if(!isLocalPlayer){return;}
        Cursor.lockState = CursorLockMode.Locked;
        controller = gameObject.GetComponent<CharacterController>();
        mainCamera = Camera.main;
        mainCamera.transform.SetParent(transform);
        mainCamera.transform.localPosition = new Vector3(0, cameraOffSetY, 0);
    }

    private void Update() 
    {
        if(!isLocalPlayer){return;}
        Main();
    }
    private void Main()
    {
        //inputs
        float MoveX = Input.GetAxis("Horizontal");
        float MoveZ = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X") * mouseSensX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensY;

        //Move
        Vector3 move = transform.right * MoveX + transform.forward * MoveZ;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        
        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
