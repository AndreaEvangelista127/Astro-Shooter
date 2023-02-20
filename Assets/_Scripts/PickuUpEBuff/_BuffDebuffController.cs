using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BuffDebuffController : MonoBehaviour
{
    [Header("ModificheBuff")]
    public float nuoviDanni;
    public float nuovaVelocita;

    [Header("TempiBuff")]
    public int tempoBuffDanni;
    public int tempoBuffVelocita;
    public int tempoBuffStella;
    public int tempoBuffMulti;

    [Header("BuffStella")]
    public float tempoStella;   //tempo invulnerabilità 
    public int numberOfFlashes;    //numero di flash dopo invulnerabilità

    private SpriteRenderer sR;

    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        
    }

    public void BuffdamageMultiplier()
    {
        StartCoroutine(BuffDanni());
    }

    public void BuffvelocityMultiplier()
    {
        StartCoroutine(BuffVelocita());
    }

    public void BuffStella()
    {
        StartCoroutine(BuffCoStella());
    }

    public void BuffMultiSparo()
    {
        StartCoroutine(MultiSparo());
    }

    private IEnumerator BuffDanni()
    {
        float danniAttuali = 1;
        this.GetComponentInChildren<Weapon>().buffDanni(nuoviDanni);
        yield return new WaitForSeconds(tempoBuffDanni);
        this.GetComponentInChildren<Weapon>().coloreNormale();
        this.GetComponentInChildren<Weapon>().setDamage(danniAttuali);
    }

    private IEnumerator BuffVelocita()
    {
        float velAttuale = this.GetComponent<PlayerMovementConAnimazione>().runSpeed;
        this.GetComponent<PlayerMovementConAnimazione>().setVelocita(nuovaVelocita);
        yield return new WaitForSeconds(tempoBuffVelocita);
        this.GetComponent<PlayerMovementConAnimazione>().setVelocita(velAttuale);
    }

    private IEnumerator BuffCoStella()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);  //metti i due layer come parametri che non devono collidere (così che magari non trapassa i muri), che sono player e enemy


        for (int i = 0; i < numberOfFlashes; i++)
        {
            sR.color = new Color(255, 255, 0, 255);  //cambio colore in rosso

            yield return new WaitForSeconds(tempoStella / (numberOfFlashes * 2));  //così si aspett aprima che il codce fa eseguire la prossima linea di codice

            sR.color = new Color(255, 255, 255, 255);

            yield return new WaitForSeconds(tempoStella / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(6, 7, false);  //metti i due layer come parametri che non devono collidere (così che magari non trapassa i muri), che sono player e enemy


    }



    private IEnumerator MultiSparo()
    {
        GetComponentInChildren<Weapon>().enabled = false;
        GetComponentInChildren<WeaponMulti>().enabled = true;
        yield return new WaitForSeconds(tempoBuffMulti);
        GetComponentInChildren<Weapon>().enabled = true;
        GetComponentInChildren<WeaponMulti>().enabled = false;
    }

}




