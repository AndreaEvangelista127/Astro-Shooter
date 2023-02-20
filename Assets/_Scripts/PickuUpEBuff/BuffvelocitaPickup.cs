using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffvelocitaPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip speedUp;
    public float volume = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(speedUp, transform.position, volume);
            collision.GetComponent<_BuffDebuffController>().BuffvelocityMultiplier();
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
