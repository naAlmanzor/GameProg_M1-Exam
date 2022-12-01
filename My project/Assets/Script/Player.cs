using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public bool onJumpInput;
    float onHorizontalInput;
    float onVerticalInput;

    public Rigidbody rb;
    
    [SerializeField]private GameObject onGround;
    // Start is called before the first frame update
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
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(onHorizontalInput, rb.velocity.y, onVerticalInput );

        if (Physics.OverlapSphere(onGround.transform.position, 0.01f).Length == 0)        
        {
            return;
        }

        if(onJumpInput==true){
            rb.AddForce(Vector3.up*3f,ForceMode.VelocityChange);
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
}
