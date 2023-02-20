using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovementConAnimazione : MonoBehaviour
{
    [Header("Movimento")]
    public Joystick joystick;
    public float runSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    //nuovo per animare il flipping del personaggio
    public Joystick WeaponJoystick;
    Vector2 aim;

    //floating2
    [Header("FloatingEffect")]
    //public float degreesPerSecond = 15.0f;  //usato solo per la rotazione ma non ci riguarda
    public float amplitude; //0.005f
    public float frequency;  //1f
    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    //flipping
    [Header("Flipping")]
    public GameObject character;
    bool isRight = false;
    bool isLeft = true;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //valori del movimento con il joystick
        movement.x = joystick.Horizontal * runSpeed;    //ci ritorna un valore float tra -1 e 1, se andiamo a destra ritorna 1, se anidamo a sinistra ritorna -1 e se stiamo al centro ritorna 0
        movement.y = joystick.Vertical * runSpeed;

        //qui provo a flippare qunado miro con l'arma
        aim.x = WeaponJoystick.Horizontal * runSpeed;  //vedi se togliere runSpeed
        aim.y = WeaponJoystick.Horizontal * runSpeed;  // questo non serve

        if(aim.x <= -0.01f && !isLeft)
        {
            isRight = false;
            isLeft = true;
            character.transform.Rotate(new Vector3(0, 180, 0));           
        }
        else if (aim.x >= 0.01f && !isRight) 
        {
            isRight = true;
            isLeft = false;
            character.transform.Rotate(new Vector3(0, 180, 0));
            
        }

        //se non è stato dato nessun input di movimento (sta fermo), quindi fluttua
            if (movement.x == 0 && movement.y == 0)
            {
                posOffset = transform.position;
                // Float up/down with a Sin()
                tempPos = posOffset;
                tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
                transform.position = tempPos;
            }
            //se viene dato un iunput di movimento (si muove) e si muove normalmente
            else
            {
                rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
            }
    }

    //void FixedUpdate()
    //{
    //    //se non è stato dato nessun input di movimento (sta fermo), quindi fluttua
    //    if (movement.x == 0 && movement.y == 0)
    //    {
    //        posOffset = transform.position;
    //        // Float up/down with a Sin()
    //        tempPos = posOffset;
    //        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
    //        transform.position = tempPos;
    //    }
    //    //se viene dato un iunput di movimento (si muove) e si muove normalmente
    //    else
    //    {
    //        rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
    //    }

    //}

    public void setVelocita(float nuovaVelocita)
    {
        this.runSpeed = nuovaVelocita;
    }


}