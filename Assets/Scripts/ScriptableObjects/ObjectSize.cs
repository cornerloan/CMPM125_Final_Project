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
    [SerializeField] private string growthAxis; //String to determine axis of growth Strings: All, X, Y, Z

    //[SerializeField] private GameObject child; Possible field used if each object has an empty parent as a pivot point

    private Vector3 growthFactor; //Actual Vector 3 used in calculations based on growth Axis
    private bool shouldGrow;
    private bool visible;

    [SerializeField] private GameObject player;
    //[SerializeField] private bool isOnTop = false;
    [SerializeField] private bool notOnTop = true;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // Initialize size to minScale to start from the smallest size
        //transform.localScale = Vector3.one * minScale;
        if(growthAxis == "All"){
            growthFactor = Vector3.one * growthRate;
        }else if (growthAxis == "X"){
            growthFactor = new Vector3 (growthRate,0f,0f);
        }else if(growthAxis == "Y"){
            growthFactor = new Vector3 (0f,growthRate,0f);
        }else if(growthAxis == "Z"){
            growthFactor = new Vector3 (0f,0f,growthRate);
        }else{
            Debug.Log("Growth Axis not set to proper value");
            growthFactor = new Vector3 (0f,0f,0f);
        }
        Debug.Log(growthFactor.ToString());
    }

    void Update()
    {
        if (!visible)
        {
            // Growing or shrinking logic
            if (shouldGrow)
            {
                transform.localScale += growthFactor * Time.deltaTime;
                checkScale();
            }
            else
            {
                transform.localScale -= growthFactor * Time.deltaTime;
                checkScale();
            }
            /* Kinda works, should take out of final implementation if real solution found
            if (isOnTop)
            {
                player.transform.position += transform.localScale;
                player.transform.position += transform.position;
                player.transform.position += new Vector3(0, 10f, 0);
            }
            */
        }
    }

    private void checkScale(){
        if(shouldGrow){
            if(growthAxis == "Y"){
                if (transform.localScale.y >= maxScale) shouldGrow = false;
            }else if(growthAxis == "Z"){
                if (transform.localScale.z >= maxScale) shouldGrow = false;
            }else{
                if (transform.localScale.x >= maxScale) shouldGrow = false;
            }
        }else{
            if(growthAxis == "Y"){
                if (transform.localScale.y <= minScale) shouldGrow = true;
            }else if(growthAxis == "Z"){
                if (transform.localScale.z <= minScale) shouldGrow = true;
            }else{
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
        
        do
        {
            if (!Physics.Raycast(player.transform.position, Vector3.down, out RaycastHit hitInfo1, Mathf.Infinity, groundLayer))
            {
                player.transform.position += new Vector3(0, 0.5f, 0);
            }
            else notOnTop = false;
        } while (notOnTop);
        notOnTop = true;
        
    }

    private void OnBecameVisible()
    {
        visible = true;
        //Debug.Log("seen");
        // Ensure the object is hovering at hoverHeight above the ground
        AdjustHeightAboveGround();
    }

    private void OnBecameInvisible()
    {
        visible = false;
        //Debug.Log("unseen");
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform.position.y > transform.position.y)
        {
            isOnTop = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnTop = false;
        }
    }
    */
}
