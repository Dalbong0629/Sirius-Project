using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTestDamage : MonoBehaviour
{
    public NPCHealth npcHealth; // Inspector에서 NPCHealth 연결

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // K 누르면 데미지 1
        {
            if (npcHealth != null) npcHealth.TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.L)) // L 누르면 회복 1
        {
            if (npcHealth != null) npcHealth.Heal(1);
        }
    }
}

