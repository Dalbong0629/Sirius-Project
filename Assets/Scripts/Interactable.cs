using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("오브젝트 이름")]
    public string objectName = "Interactable Object";

    [Header("상호작용 효과")]
    public Color highlightColor = Color.yellow;

    private Renderer objRenderer;
    private Color originalColor;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        if (objRenderer != null)
            originalColor = objRenderer.material.color;
    }

    // PlayerController에서 호출
    public void Interact()
    {
        Debug.Log(objectName + "와 상호작용함!");

        // 예시: 상호작용 시 색상 깜빡이기
        if (objRenderer != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashColor());
        }
    }

    private System.Collections.IEnumerator FlashColor()
    {
        objRenderer.material.color = highlightColor;
        yield return new WaitForSeconds(0.3f);
        objRenderer.material.color = originalColor;
    }
}
