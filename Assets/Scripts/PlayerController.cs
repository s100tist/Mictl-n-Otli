using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private readonly float jumpForce = 6;
    private readonly float movementSpeed = 5.2f;
    private bool isGrounded = false;
    private float currentVelocity;
    public GameObject mensajeFinal;

    void Start()
    {
        transform.position = new Vector3(-1.1f, -1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move player left and right
        float horizontalInput = Input.GetAxis("Horizontal");
        float targetVelocity = horizontalInput * movementSpeed;
        float smoothVelocity = Mathf.SmoothDamp(GetComponent<Rigidbody2D>().velocity.x, targetVelocity, ref currentVelocity, 0.05f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(smoothVelocity, GetComponent<Rigidbody2D>().velocity.y);

        // Jump player with up arrow
        //bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            isGrounded = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if player is on ground layer
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tecpatl"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Limite Muerte"))
        {
            //Destroy(gameObject);
            //GameObject mensajeFinal = GameObject.FindGameObjectWithTag("Mensaje Final");
            mensajeFinal.SetActive(true);
        }
    }
}
