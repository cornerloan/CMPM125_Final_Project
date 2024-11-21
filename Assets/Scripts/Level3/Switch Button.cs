using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] GameObject[] walls;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < walls.Count(); i++)
        {
            if (walls[i].GetComponent<Wall>().wallState.ToString() == "blue")
            {
                walls[i].GetComponent<Wall>().wallState = Wall.State.orange;
            } else
            {
                walls[i].GetComponent<Wall>().wallState = Wall.State.blue;
            }
        }
    }
}
