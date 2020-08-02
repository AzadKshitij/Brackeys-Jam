using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100.0f;
    [SerializeField] private GameObject body;
    private Rigidbody2D playerRB;
    private Vector3 moveDir;
    private bool isDashing = false;

    


    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = 0.0f;
        float moveY = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.transform.localScale = new Vector3(-1, 1, 1);
            moveX = -1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.transform.localScale = new Vector3(1, 1, 1);
            moveX = 1.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
        }

        moveDir = new Vector3(moveX, moveY, 0).normalized;
    }

    private void FixedUpdate()
    {
        float dashAmount = 2.0f;
        playerRB.velocity = moveDir * speed * Time.deltaTime;
        if(isDashing)
        {
            playerRB.MovePosition(transform.position + moveDir * dashAmount);
            isDashing = false;
        }
    }
}
