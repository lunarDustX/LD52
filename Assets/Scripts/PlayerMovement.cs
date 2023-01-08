using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 2f;

    private float h, v;
    private Rigidbody2D rb;

    private Vector2 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        moveDir = new Vector2(h, v).normalized;

        if (moveDir != Vector2.zero)
        {
            transform.up = moveDir;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = speed * moveDir;

    }
}
