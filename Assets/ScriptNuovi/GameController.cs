using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayFabManager PFM;
    public ScoreManager SM;

    public GameObject GameOverText;

    public AudioSource GameOverSound;
    public AudioSource BgAudio;

    public void GameOver()
    {
        
        Debug.Log("Game OVer");
        //GameObject testo = GameOverText;
        //GameOverText.GetComponent<TextMesh>().text = ("Game Over");
        //GameOverText.SetActive(true);
        GameOverText.SetActive(true);
        GameOverSound.Play();
        BgAudio.Stop();
        Time.timeScale = 0.3f;


        if (SM.score > SM.highScore)
            PFM.SendLeaderBoard(SM.score);

        GameObject player = GameObject.FindGameObjectWithTag("Player");    //In target ci va il game object che equivale al personaggio
        if (player.GetComponent<health>().isDead == false)
        {
            player.GetComponent<health>().isDead = true;
        }
    }


   
}
