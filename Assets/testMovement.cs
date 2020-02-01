using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMovement : MonoBehaviour
{

    private Rigidbody rb;

    public float jumpForce = 1f;
    public float speed = 5f;

    // checks if key was already pressed
    public bool forwardCond = false;
    public bool backCond = false;
    public bool leftCond = false;
    public bool rightCond = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!forwardCond && Input.GetKey(KeyCode.W))
        {
            //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
            forwardCond = true;
            rb.velocity += Vector3.forward * speed;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            forwardCond = false;
            float x = rb.velocity.x;
            float y = rb.velocity.y;
            float z = 0;
            rb.velocity = new Vector3(x, y, z);
        }
        if (!leftCond && Input.GetKey(KeyCode.A))
        {
            //rb.AddForce(-sidewayForce * Time.deltaTime, 0, 0);
            leftCond = true;
            rb.velocity += Vector3.left * speed;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            leftCond = false;
            float x = 0;
            float y = rb.velocity.y;
            float z = rb.velocity.z;
            rb.velocity = new Vector3(x, y, z);
        }
        if (!rightCond && Input.GetKey(KeyCode.D))
        {
            //rb.AddForce(sidewayForce * Time.deltaTime, 0, 0);
            rightCond = true;
            rb.velocity += Vector3.right * speed;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rightCond = false;
            float x = 0;
            float y = rb.velocity.y;
            float z = rb.velocity.z;
            rb.velocity = new Vector3(x, y, z);
        }
        if (!backCond && Input.GetKey(KeyCode.S))
        {
            //rb.AddForce(0, 0, -forwardForce * Time.deltaTime);
            backCond = true;
            rb.velocity += Vector3.back * speed;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            backCond = false;
            float x = rb.velocity.x;
            float y = rb.velocity.y;
            float z = 0;
            rb.velocity = new Vector3(x, y, z);
        }
        if (isGrounded() && Input.GetKey(KeyCode.Space))
        {
            //rb.AddForce(0, jumpForce * Time.deltaTime, 0);
            rb.velocity += Vector3.up * jumpForce;
        }
    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }
}
