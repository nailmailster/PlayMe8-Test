using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    public delegate void SwipeHandler(float deltaX);
    public event SwipeHandler OnSwipe;

    float xBegan = 0f, xEnded = 0f, xDelta = 0f;

    void Update()
    {
        Swipe();
    }

    void Swipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                xBegan = touch.position.x;
            if (touch.phase == TouchPhase.Ended)
            {
                xEnded = touch.position.x;
                xDelta = xEnded - xBegan;
                if (xDelta != 0)
                    if (OnSwipe != null)
                        OnSwipe(xDelta);
            }
        }
    }
}
