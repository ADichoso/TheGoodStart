using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Vector3 RightOffset, LeftOffset;

    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        RightOffset = new Vector3(-rectTransform.rect.width / 2, -rectTransform.rect.height / 2, 0);
        LeftOffset = new Vector3(rectTransform.rect.width / 2, -rectTransform.rect.height / 2, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.mousePosition.x > Screen.width / 2)
        {
            transform.position = Vector3.Lerp(transform.position, Input.mousePosition + RightOffset, Time.deltaTime);
        }   
        else
        {
            transform.position = Vector3.Lerp(transform.position, Input.mousePosition + LeftOffset, Time.deltaTime);
        }
    }
}

/*
float newXPosition = Mathf.Clamp(transform.position.x + (difference.x / offsetFactor), initialPosition.x - maxShift.x, initialPosition.x + maxShift.x);
        float newYPosition = Mathf.Clamp(transform.position.y + (difference.y / offsetFactor), initialPosition.y - maxShift.y, initialPosition.y + maxShift.y);
        transform.position = Vector3.Lerp(transform.position, new Vector3(newXPosition, newYPosition, transform.position.z), Time.deltaTime);
*/
