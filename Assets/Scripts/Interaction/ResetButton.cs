using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform child;
    [SerializeField] private String description;

    // Update is called once per frame
    public void Interact(){
        child.transform.localScale = Vector3.one;
    }

    public string GetDescription(){
        return description;
    }
}
