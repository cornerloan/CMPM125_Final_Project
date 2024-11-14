using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPosition : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 position1;
    [SerializeField] private Vector3 position2;
    private bool shouldMoveTo2;
    private bool visible;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = position1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!visible)
        {
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
        }
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
