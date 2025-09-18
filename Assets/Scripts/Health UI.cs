using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health health;   // Player Health 연결
    public Image hpFill;    // HP바 Image 연결

    void Update()
    {
        if (health != null && hpFill != null)
        {
            hpFill.fillAmount = (float)health.currentHealth / health.maxHealth;
        }
    }
    
}




