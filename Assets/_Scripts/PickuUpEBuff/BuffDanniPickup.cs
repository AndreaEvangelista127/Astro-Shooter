using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDanniPickup : MonoBehaviour
{
    public AudioClip dmgUp;
    public float volume = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(dmgUp, transform.position, volume);
            collision.GetComponent<_BuffDebuffController>().BuffdamageMultiplier();           
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
