using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Joystick joystick;
    public float runSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = joystick.Horizontal * runSpeed;    //ci ritorna un valore float tra -1 e 1, se andiamo a destra ritorna 1, se anidamo a sinistra ritorna -1 e se stiamo al centro ritorna 0
        movement.y = joystick.Vertical * runSpeed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
    }
}