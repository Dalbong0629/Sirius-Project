using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCHealthUI : MonoBehaviour
{
    [Header("체력바 이미지")]
    public Image hpFill;         // HP 이미지 Fill

    [Header("머리 위 위치 조정")]
    public Vector3 offset = new Vector3(0, 2f, 0);  // NPC 머리 위 위치

    private Transform target;    // NPC 위치 따라가기

    void Start()
    {
        target = transform.parent; // Canvas가 NPC 자식이면 부모(NPC)를 따라감
    }

    void LateUpdate()
    {
        if (target != null)
            transform.position = target.position + offset;
    }

    // HP바 갱신
    public void SetHealth(int current, int max)
    {
        if (hpFill != null)
            hpFill.fillAmount = (float)current / max;
    }
}
