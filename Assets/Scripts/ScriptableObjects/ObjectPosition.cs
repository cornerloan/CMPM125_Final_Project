using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPosition : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 position1;
    [SerializeField] private Vector3 position2;
    [SerializeField] private Transform player; 
    [SerializeField] private float playerDistanceThreshold = 2f;

    private bool shouldMoveTo2;
    private bool visible;

    void Start()
    {
        transform.position = position1;
    }

    void Update()
    {
        if (!visible)
        {
            // move between positions
            if (shouldMoveTo2)
            {
                transform.position = Vector3.MoveTowards(transform.position, position2, moveSpeed * Time.deltaTime);
                if (transform.position == position2) shouldMoveTo2 = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, position1, moveSpeed * Time.deltaTime);
                if (transform.position == position1) shouldMoveTo2 = true;
            }

            /**
            // make the player a child of the moving platform, to keep the player on top
            if (IsPlayerOnTop())
            {
                player.SetParent(transform); // Make the player a child of the object
            }
            else
            {
                player.SetParent(null); // Unparent the player
            }
            */
        }
    }

    private bool IsPlayerOnTop()
    {
        // if the player is above the object and within playerDistanceThreshold of the object
        float distanceToObject = Vector3.Distance(player.position, transform.position);
        return distanceToObject <= playerDistanceThreshold && player.position.y >= transform.position.y;
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }
}
