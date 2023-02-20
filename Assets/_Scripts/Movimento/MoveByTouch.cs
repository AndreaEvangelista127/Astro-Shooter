using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)   //Vediamo se ci sono stati tocchi sullo schermo
        {
            Touch touch = Input.GetTouch(0);  //Inserisco le informazioni del primo(0) tocco in una variabile (touch)
            //touch.position  //Posso sapere la posizione del tocco sullo schermo in coordinate in pixel
            //touch.phase  //Posso sapere le fasi in cui si trova il tocco, iniziale, finito, si sta muovendo, stazionario, cancellato

            //Per muovere l'oggetto nel posizione del punto corrente uso touch.position
            //L'oggetto è in coordinate del mondo mentre il tocco è in coordinate in pixel, quindi dobbiamo convertire da screen a world space

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);  //usiamo la main camera e mettiamo questa posizione in un vettore con tre coordinate x,y,z

            touchPosition.z = 0f;
            transform.position = touchPosition;  //Settiamo la posizione del nostro oggetto con la posizione del tocco sullo schermo, ma questo setta la posizione del nostro tocco sulla z della stessa posizione come la nostra camera
        }                                       //Ma noi non vogliamo questo, quindi settiamo la z su 0

        //Trattiamo tutti i tocchi sullo schermo e non solo il primo

        /*for (int i = 0; i < Input.touchCount; i++) {    //Array, cioè una lista di tutti i tocchi

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero, touchPosition, Color.red);     //Crea una linea che parte dal centro dell'oggetto al tocco di colore rosso
        }*/
    }
}
