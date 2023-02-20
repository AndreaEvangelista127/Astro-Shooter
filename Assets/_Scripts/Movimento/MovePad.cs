using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MovePad : MonoBehaviour
    {
        public float moveSpeed = 5f;

        public Rigidbody2D rb;

        public Joystick joystick;

        Vector2 movement;

        // Update is called once per frame
        void Update()
        {
            //movement.x = Input.GetAxisRaw("Horizontal");

            movement.x = joystick.Horizontal * moveSpeed;

            //movement.y = Input.GetAxisRaw("Vertical");

            movement.y = joystick.Vertical * moveSpeed;
        }

        void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

    }
