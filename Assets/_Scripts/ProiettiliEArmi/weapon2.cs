using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : MonoBehaviour
{

    public Joystick joystick;
    public float offset;
    Vector2 movement;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 200f;

    public float fireRate = 1f;
    private float nextFire = 0f;

    // Update is called once per frame
    void Update()
    {

        movement.x = joystick.Horizontal;    //ci ritorna un valore float tra -1 e 1, se andiamo a destra ritorna 1, se anidamo a sinistra ritorna -1 e se stiamo al centro ritorna 0
        movement.y = joystick.Vertical;

        //Vector3 difference = Camera.main.ScreenToWorldPoint();
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            float rotZ = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }

        if (((movement.x > 0.5) || (movement.x < -0.5) || (movement.y > 0.5) || (movement.y < -0.5)) && Time.time > nextFire)
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

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Proiettile Amico")
        {
            Destroy(collision.gameObject);  //così disatruggo ogni oggetto che tocca il proiettile
            //Destroy(this.gameObject);
        }
    }*/
}