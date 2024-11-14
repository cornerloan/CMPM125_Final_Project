using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSize : MonoBehaviour
{
    [SerializeField] private float growthRate;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float hoverHeight = 0f; // Distance to keep above the ground
    [SerializeField] private LayerMask groundLayer;    // Layer mask for detecting ground
    private bool shouldGrow;
    private bool visible;

    void Start()
    {
        // Initialize size to minScale to start from the smallest size
        transform.localScale = Vector3.one * minScale;
    }

    void Update()
    {
        if (!visible)
        {
            // Growing or shrinking logic
            if (shouldGrow)
            {
                transform.localScale += Vector3.one * growthRate * Time.deltaTime;
                if (transform.localScale.x >= maxScale) shouldGrow = false;
            }
            else
            {
                transform.localScale -= Vector3.one * growthRate * Time.deltaTime;
                if (transform.localScale.x <= minScale) shouldGrow = true;
            }

        }
    }

    private void AdjustHeightAboveGround()
    {
        // Cast a ray downward to detect the ground
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
        {
            //height is hoverHeight above ground (prevent clipping issues)
            transform.position = new Vector3(
                transform.position.x,
                hitInfo.point.y + hoverHeight,
                transform.position.z
            );
        }
    }

    private void OnBecameVisible()
    {
        visible = true;
        // Ensure the object is hovering at hoverHeight above the ground
        AdjustHeightAboveGround();
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }
}
