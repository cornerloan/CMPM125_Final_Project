using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] TimeController timeScript;

    private void OnTriggerEnter(Collider other)
    {
        timeScript.updateBestTime();
        SceneManager.LoadScene("LevelSelect");
    }
}
