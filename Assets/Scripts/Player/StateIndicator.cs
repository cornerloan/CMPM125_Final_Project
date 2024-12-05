using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateIndicator : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (player.GetComponent<PlayerLevel3Mechanics>().playerState.ToString() == "blue")
        {
            image.color = new Color32(0, 32, 255, 255); // blue
        }
        else
        {
            image.color = new Color32(255, 121, 0, 255); // orange
        }


    }
}
