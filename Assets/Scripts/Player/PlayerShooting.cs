using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Transform bodyTransform;
    private Vector3 initialScale;

    private void Awake()
    {
        bodyTransform = transform.Find("Body");
        initialScale = bodyTransform.localScale;
    }

    private void Update()
    {
        // Updates the mouse position
        Vector3 mousePosition = GetMouseWorldPosition();

        // makes the player sprite face towards the mouse position
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        bodyTransform.eulerAngles = new Vector3(0, 0, angle);

        // Determines if the sprite should be flipped or not
        FlipSprite(angle);
    }

    private void FlipSprite(float _angle)
    {
        if (_angle > 90 || _angle < -90)
        {
            Vector3 theScale = initialScale;
            theScale.y *= -1;
            theScale.z *= -1;
            bodyTransform.localScale = theScale;
        }
        else
        {
            bodyTransform.localScale = initialScale;
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }
}
