using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileStraight : MonoBehaviour
{

    public float speed = 2f;
    public Vector2 target;  //direzione di dove si muove
    public float timeToLive = 2;
    public float dannoProiettile = 1;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", timeToLive);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(target * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Giocatore colpito");
        if (collision.tag == "Player")
        {
            collision.GetComponent<health>().takeDamge(dannoProiettile);
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Meteor")
        {
            Destroy(this.gameObject);
        }
    }

    public void setTarget(Vector2 target)
    {
        this.target = target;
    }
}
