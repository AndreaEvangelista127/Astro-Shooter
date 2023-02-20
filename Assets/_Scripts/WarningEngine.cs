using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningEngine : MonoBehaviour
{
    public static WarningEngine instance;

    public bool trigger = false;

    public float timer = 10f;

    public Text timerText;

    public GameObject uiArt;

    public GameController GC;

    void Awake()
    {
        instance = this;
    }

    public void enterArea()
    {
        uiArt.SetActive(true);
        trigger = true;
    }

    public void exitArea()
    {
        trigger = false;
        uiArt.SetActive(false);
        timer = 10f;
    }

    void Update()
    {
        if (trigger)
            timer -= Time.deltaTime;
        timerText.text = timer.ToString();
        //Debug.Log("CACCA");

        if (timer <= 0)
        {
            GC.GameOver();
            print("Dead");
            trigger = false;
        }
    }

}