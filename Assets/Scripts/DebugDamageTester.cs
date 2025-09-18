using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDamageTester : MonoBehaviour
{
    public Health playerHealth;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // K 누르면 데미지 1
        {
            if (playerHealth != null) playerHealth.TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.L)) // L 누르면 회복 1
        {
            if (playerHealth != null) playerHealth.Heal(1);
        }
    }
}
