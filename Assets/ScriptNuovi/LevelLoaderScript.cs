using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void PlayGame()
    {
        LoadLevel();
        /* Carica la prossima scene facendo quella attuale +1(ho settato nei build settings che la main menu è la 0 ed il game 1) */
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /* couroutine per l'animazione crossFade*/
        public IEnumerator LoadLevel()
        {

            transition.SetTrigger("Start"); //start sarebbe il parametro usato nella animazione per dare modo all'animazione di partire

            yield return new WaitForSeconds(transitionTime);

        }

        public void QuitGame()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }

    }
