using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateTutorials : MonoBehaviour
{
    [SerializeField] TMP_Text textToChange;
    [SerializeField] string changeTo;

    private void OnTriggerEnter(Collider other)
    {
        textToChange.text = changeTo;
    }
}
