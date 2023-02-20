using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMulti : MonoBehaviour
{

    public Joystick joystick;
    public float offset;
    Vector2 aim;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 200f;
    public float fireRate = 1f;
    private float nextFire = 0f;

    //nuovi per il multiFire
    public int numeroProiettili;
    public float spreadProiettili;

    // Update is called once per frame
    void Update()
    {

        aim.x = joystick.Horizontal;    //ci ritorna un valore float tra -1 e 1, se andiamo a destra ritorna 1, se anidamo a sinistra ritorna -1 e se stiamo al centro ritorna 0
        aim.y = joystick.Vertical;

        //NUOVO, rotazione completa dell'arma
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            float rotZ = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            if (aim.x <= 0)
                transform.rotation = Quaternion.Euler(0, 0, rotZ + offset);  //riattiva per far funzionare
            else
                transform.rotation = Quaternion.Euler(180, 0, -rotZ + offset);  //riattiva per far funzionare

        }

        //superata una soglia spara
        if (((aim.x > 0.5) || (aim.x < -0.5) || (aim.y > 0.5) || (aim.y < -0.5)) && Time.time > nextFire)
        {
            Shoot();
        }


        void Shoot()
        {
            nextFire = Time.time + fireRate;

            //si prende nota di dove stia guardando il giocatore
            float facingRotation = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;

            //nuovi
            float startRotation = facingRotation + spreadProiettili / 2f;
            Debug.Log("facing rotation is :" + facingRotation);
            float angleIncrease = spreadProiettili / ((float)numeroProiettili - 1f);
            Debug.Log("angle increase is : "+ angleIncrease);

            for ( int i=0; i < numeroProiettili; i++)
            {
                float tempRot = startRotation - angleIncrease * i;
                Debug.Log("temp rot" + i + ": " + tempRot);

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                //RUOTAVA MALE I PROIETTILI
                //GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, tempRot));
                
                Vector2 direzione = new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad), Mathf.Sin(tempRot * Mathf.Deg2Rad));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(direzione * bulletForce, ForceMode2D.Impulse);
                
            }
        }
    }

    public void setDamage(float danni)
    {
        this.bulletPrefab.GetComponent<ProiettileAmico>().dannoProiettile = danni;
        Debug.Log(this.bulletPrefab.GetComponent<ProiettileAmico>().dannoProiettile);
    }

}