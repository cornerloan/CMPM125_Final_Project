using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour, IInteractable
{
    public GameObject[] children;
    [SerializeField] private String description;

    // Update is called once per frame
    public void Interact(){
        Debug.Log("click");
        foreach( GameObject child in children )
            child.transform.localScale = Vector3.one;
    }

    public string GetDescription(){
        return description;
    }
}