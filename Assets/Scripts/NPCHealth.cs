using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    [Header("NPC 체력 설정")]
    public int maxHealth = 5;      // NPC마다 Inspector에서 다르게 설정 가능
    public int currentHealth;

    [Header("UI 연결")]
    public NPCHealthUI healthUI;   // 머리 위 HP바 UI

    void Start()
    {
        currentHealth = maxHealth;

        // UI 초기화
        if (healthUI != null)
            healthUI.SetHealth(currentHealth, maxHealth);
    }

    // 데미지 받기
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        // UI 업데이트
        if (healthUI != null)
            healthUI.SetHealth(currentHealth, maxHealth);

        if (currentHealth == 0)
            Die();
    }

    // 회복
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        if (healthUI != null)
            healthUI.SetHealth(currentHealth, maxHealth);
    }

    void Die()
    {
        // NPC 사망 처리 (Destroy 등)
        Destroy(gameObject);
    }
}
