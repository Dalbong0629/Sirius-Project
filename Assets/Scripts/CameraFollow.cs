using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
       public Transform target;
    public Vector3 defaultOffset = new Vector3(0, 5, -7);
    public Vector3 lookDownOffset = new Vector3(0, 2, -7); // ¾Æ·¡º¸±â ½Ã Ä«¸Þ¶ó À§Ä¡
    public float smoothSpeed = 5f;
    public KeyCode ¾Æ·¡¸¦ÃÄºÁ = KeyCode.C; // ¤µ»ß¤¿¤©¾¾¹ß¾¾»¡¾¾¹ß¾¾¹ß¾¾¹ß¾¾¹ß¾¾¹ß

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
                target = player.transform;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredOffset = defaultOffset;

            // ¾Æ·¡º¸±â Å° ÀÔ·Â ½Ã offset º¯°æ
            if (Input.GetKey(¾Æ·¡¸¦ÃÄºÁ))
            {
                desiredOffset = lookDownOffset;
            }

            Vector3 desiredPosition = target.position + desiredOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
