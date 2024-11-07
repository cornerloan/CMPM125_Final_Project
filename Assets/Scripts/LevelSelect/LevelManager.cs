using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Transform level;
    private RaycastHit hit; 

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
        {
            level = hit.transform;
            if (level.CompareTag("Selectable"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    switch (level.name)
                    {
                        case "Level1":
                            SceneManager.LoadScene("TestScene");
                            break;
                        case "Level2":

                            break;
                        case "Level3":

                            break;
                        case "Level4":

                            break;
                        case "Level5":

                            break;
                        case "Level6":

                            break;
                    }
                }
            }
        }
    }



}
