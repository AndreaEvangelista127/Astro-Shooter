using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Joystick joystick;
    public float offset;
    Vector2 aim;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 200f;

    public float fireRate = 1f;
    private float nextFire = 0f;


    // Update is called once per frame
    void Update()
    {

        aim.x = joystick.Horizontal;    //ci ritorna un valore float tra -1 e 1, se andiamo a destra ritorna 1, se anidamo a sinistra ritorna -1 e se stiamo al centro ritorna 0
        aim.y = joystick.Vertical;

        //VECCHIO, ROTAZIONE PARZIALE DELL'ARMA
        //Vector3 difference = Camera.main.ScreenToWorldPoint();
        //if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        //{
        //    float rotZ = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        //}

        //NUOVO, rotazione completa dell'arma
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            float rotZ = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            if(aim.x <= 0)
            transform.rotation = Quaternion.Euler(0, 0, rotZ + offset);  //riattiva per far funzionare
            else
            transform.rotation = Quaternion.Euler(180, 0, -rotZ + offset);  //riattiva per far funzionare

        }

        //superata una soglia spara
        if (((aim.x > 0.5 ) || (aim.x < -0.5) || (aim.y > 0.5) || (aim.y < -0.5)) && Time.time > nextFire)
        {
            Shoot();
        }

      
        void Shoot()
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    public void setDamage(float danni)
    {
        this.bulletPrefab.GetComponent<ProiettileAmico>().dannoProiettile = danni;
        
    }

    //quetse due fuznioni qui sotto sono usate per il buff dei danni e la modifica del colore dell'arma
    public void buffDanni(float danni)
    {
        this.bulletPrefab.GetComponent<ProiettileAmico>().dannoProiettile = danni;
        this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
    }

    public void coloreNormale()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

}