using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [Header("HP 설정")]
    public int maxHealth = 5;          // Inspector에서 설정 가능
    [HideInInspector] public int currentHealth;

    [Header("HP UI 옵션")]
    public bool useWorldSpaceUI = false;   // (선택) NPC는 true, 플레이어는 false
    public Transform hpBarParent;          // UI 아이콘 부모 (Horizontal Layout Group 등)
    public GameObject hpIconPrefab;        // 하트 아이콘 Prefab (UI Image)

    private List<GameObject> hpIcons = new List<GameObject>();

    void Start()
    {
        // 초기 HP (필요하면 세이브 로드 후 덮어쓰지 않도록 조절 가능)
        currentHealth = Mathf.Clamp(currentHealth == 0 ? maxHealth : currentHealth, 0, maxHealth);

        // UI 초기화 (hpBarParent와 prefab이 연결되어 있을 때만)
        if (hpBarParent != null && hpIconPrefab != null)
        {
            InitHPUI();
            UpdateHPUI();
        }
    }

    void InitHPUI()
    {
        // 기존 자식 삭제 (중복 방지)
        foreach (Transform child in hpBarParent)
        {
            Destroy(child.gameObject);
        }
        hpIcons.Clear();

        // maxHealth 만큼 아이콘 생성
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject icon = Instantiate(hpIconPrefab, hpBarParent);
            hpIcons.Add(icon);
        }
    }

    // 데미지 (한 번에 여러 칸도 가능)
    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (hpBarParent != null)
            UpdateHPUI();

        if (currentHealth <= 0)
            Die();
    }

    // 회복: DebugDamageTester에서 호출하는 메서드 (public)
    public void Heal(int amount)
    {
        if (amount <= 0) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (hpBarParent != null)
            UpdateHPUI();
    }

    void UpdateHPUI()
    {
        for (int i = 0; i < hpIcons.Count; i++)
        {
            hpIcons[i].SetActive(i < currentHealth);
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " 사망!");
        if (!CompareTag("Player"))
        {
            Destroy(gameObject); // NPC는 제거
        }
        else
        {
            // 플레이어 사망 시 처리: 예) 게임오버 화면, 리스폰 등
            Debug.Log("Player Game Over 처리 필요");
        }
    }

    // (옵션) 외부에서 현재 체력을 설정할 때 유틸로 사용
    public void SetHealth(int value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
        if (hpBarParent != null) UpdateHPUI();
    }
}
