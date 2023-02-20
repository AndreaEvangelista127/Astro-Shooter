using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    // Start is called before the first frame update

    public float valoreRecuperato;
   
    public AudioClip healthUp;
    public float volume = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(healthUp, transform.position, volume);
            //healthup.Play();
            collision.GetComponent<health>().AddHealth(valoreRecuperato);
            gameObject.SetActive(false);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
