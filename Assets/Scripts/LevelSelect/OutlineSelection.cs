using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

// Credits on how to use
// https://youtu.be/qYnAkMGbgwo?feature=shared
public class OutlineSelection : MonoBehaviour
{
    private Transform hightlight;
    private Transform selection;
    private RaycastHit hit;

    private void Update()
    {
        // highlight
        if (hightlight != null)
        {
            hightlight.gameObject.GetComponent<Outline>().enabled = false;
            hightlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit)) 
        {
            hightlight = hit.transform;
            if (hightlight.CompareTag("Selectable") && hightlight != selection)
            {
                if (hightlight.gameObject.GetComponent<Outline>() != null)
                {
                    hightlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = hightlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    hightlight.gameObject.GetComponent<Outline>().OutlineColor = Color.yellow;
                    hightlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;

                }
            }
            else
            {
                hightlight = null;
            }
        }
    }
}
