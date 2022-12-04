using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [Header("Movement")]
    float movementSpeed = 3f;

    public bool onJumpInput;
    public float jumpCooldown;
    bool readyToJump = true;

    public Transform orientation;

    float onHorizontalInput;
    float onVerticalInput;

    Vector3 movementDirection;
    public Transform playerObj;

    public Rigidbody rb;
          
    [SerializeField] GameObject onGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        PlayerInput();   

        if(Input.GetKeyDown(KeyCode.Space) && readyToJump){

            readyToJump = false;
            Jump();

            Invoke(nameof(ReadyJump), jumpCooldown);
        }
    }

    
    public void PlayerInput(){
        onHorizontalInput = Input.GetAxis("Horizontal");
        onVerticalInput = Input.GetAxis("Vertical");    
    }

    private void PlayerMovement(){
        movementDirection = orientation.forward * onVerticalInput + orientation.right * onHorizontalInput;  
    }

    private void Jump(){
        onJumpInput = false;
        rb.AddForce(Vector3.up*4f,ForceMode.VelocityChange);
    }

    private void ReadyJump(){
        readyToJump = true;
    }

    void FixedUpdate()
    {
        PlayerMovement();
        rb.MovePosition(rb.position+movementDirection*movementSpeed*Time.fixedDeltaTime);
        if (Physics.OverlapSphere(onGround.transform.position, 0.01f).Length == 1)        
        {
            return;
        }

        if(onJumpInput==true){
            Jump();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "coin"){
            Debug.Log("Interacting with Coin.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "pushBox"){
            Debug.Log("Interacting with Box.");
        }
    }
}
