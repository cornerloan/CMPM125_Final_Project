using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLock : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    private Vector3 lockedPos;

    // Start is called before the first frame update
    void Start()
    {
        lockedPos = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if(gameObject.transform.position != lockedPos)
        {
            gameObject.transform.position = lockedPos;
        }
    }
}
