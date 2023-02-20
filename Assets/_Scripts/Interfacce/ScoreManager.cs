using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;  //da vedere a che serve

    public Text scoreText;     //usato per il punteggio attuale
    public Text highScoreText; //usato per vedere il punteggio più alto della partita

    public int score = 0;
    public int highScore = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //usato per salvare l'highscore del giocatore
        highScore = PlayerPrefs.GetInt("highScore", 0);   //la parte 0 definisce un valore di default se non è assegnato niente

        scoreText.text = score.ToString() + " POINTS";
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();
    }


    public void AddPoint(int points)
    {
        score += points; //incremento punteggio
        scoreText.text = score.ToString() + " POINTS"; //rimostra il punteggio

        //usato per salvare l'highscore del giocatore
        if (highScore < score)
        {
            PlayerPrefs.SetInt("highScore", score); //qua dovrebbe salvare il punteggio del giocatore, da APPROFONDIRE come funziona
            //Debug.Log(PlayerPrefs.GetInt("highScore").ToString());
        }
    }
}

