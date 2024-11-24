using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FullScreenController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private ScriptableRendererFeature blueFullscreenShader;
    [SerializeField] private ScriptableRendererFeature orangeFullscreenShader;


    
    private void Start()
    {
        blueFullscreenShader.SetActive(false);
        orangeFullscreenShader.SetActive(false);
    }

    private void Update()
    {
        if (player.GetComponent<PlayerLevel3Mechanics>().playerState.ToString() == "blue") 
        {
            blueFullscreenShader.SetActive(true);
            orangeFullscreenShader.SetActive(false);
        }
        else 
        {
            blueFullscreenShader.SetActive(false);
            orangeFullscreenShader.SetActive(true);
        }
         
        
    }
}
