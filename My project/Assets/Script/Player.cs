using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public bool onJumpInput;
    float onHorizontalInput;
    float onVerticalInput;
    float movementSpeed = 3f;

    Vector3 movementDirection;
    public Transform playerObj;

    public Rigidbody rb;
          
    [SerializeField] GameObject onGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            onJumpInput = true;
        }

        onHorizontalInput = Input.GetAxis("Horizontal");
        onVerticalInput = Input.GetAxis("Vertical");

        movementDirection = new Vector3(onHorizontalInput, 0, onVerticalInput);
    }

    void FixedUpdate()
    {
        var direction = playerObj.rotation*movementDirection;

        rb.MovePosition(rb.position+direction*movementSpeed*Time.fixedDeltaTime);
        if (Physics.OverlapSphere(onGround.transform.position, 0.01f).Length == 1)        
        {
            return;
        }

        if(onJumpInput==true){
            rb.AddForce(Vector3.up*4f,ForceMode.VelocityChange);    
            onJumpInput = false;
        }
    }

    public void CreateClassMethod()
    {
        Debug.Log("You referenced a class!");
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
