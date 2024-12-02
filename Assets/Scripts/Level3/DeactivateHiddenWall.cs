using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateHiddenWall : MonoBehaviour
{
    [SerializeField] private GameObject wallToDeactivate;
    private bool triggered = false;

    private void OnTriggerExit(Collider other)
    {
        if (!triggered)
        {
            triggered = true;
            wallToDeactivate.SetActive(false);
        }
    }

}
