using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private health playerHealth;
    [SerializeField] private Image healthbar;
    [SerializeField] private Image currentHealthBar;


    // Start is called before the first frame update
    void Start()
    {
        healthbar.fillAmount = playerHealth.currentHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}