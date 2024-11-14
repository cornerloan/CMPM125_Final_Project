using UnityEngine;

public class Level : MonoBehaviour
{
    // [SerializeField] private GameObject mesh;
    [SerializeField] private GameObject meshOutline;

    private void Start()
    {
        meshOutline.SetActive(false);
    }



    private void OnMouseEnter()
    {
        meshOutline.SetActive(true);
    }

    private void OnMouseExit()
    {
        meshOutline.SetActive(false);
    }
}
