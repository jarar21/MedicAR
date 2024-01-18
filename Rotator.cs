using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotator : MonoBehaviour
{

    private float rotationSpeed = 0.5f;
    private Vector2 touchStartPos;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    Vector2 delta = touch.position - touchStartPos;
                    float rotationX = delta.y * rotationSpeed;
                    float rotationY = -delta.x * rotationSpeed;

                    // Rotate the object
                    transform.Rotate(Vector3.up, rotationY, Space.World);
                    transform.Rotate(Vector3.right, rotationX, Space.World);

                    touchStartPos = touch.position;
                    break;
            }
        }
    }

}
