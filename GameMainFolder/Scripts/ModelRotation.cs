using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModelRotation : MonoBehaviour,IDragHandler
{
    

    
    private Vector2 startTouchPosition;
    public  Transform modelTransform;
    private float rotationSpeed = 1.0f;

    private void Start()
    {
        // Get a reference to the 3D model's transform
         // Replace with your model's transform reference
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount == 1)
        {
            // Calculate the rotation angles based on swipe direction.
            float rotationX = eventData.delta.y * rotationSpeed;
            float rotationY = -eventData.delta.x * rotationSpeed;

            // Apply the rotations to the model transform.
            modelTransform.Rotate(Vector3.right, -rotationX, Space.World);
            modelTransform.Rotate(Vector3.up, rotationY, Space.World);
        }
    }


}
