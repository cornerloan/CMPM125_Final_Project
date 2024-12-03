using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public Material outlineMaterial; // Assign the outline material here
    private Material[] originalMaterials;
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterials = objectRenderer.sharedMaterials;
        }
    }

    public void EnableOutline()
    {
        if (objectRenderer == null || outlineMaterial == null) return;

        // Add the outline material
        Material[] materialsWithOutline = new Material[originalMaterials.Length + 1];
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            materialsWithOutline[i] = originalMaterials[i];
        }
        materialsWithOutline[originalMaterials.Length] = outlineMaterial;

        objectRenderer.materials = materialsWithOutline;
    }

    public void DisableOutline()
    {
        if (objectRenderer == null) return;

        // Revert to the original materials
        objectRenderer.materials = originalMaterials;
    }
}
