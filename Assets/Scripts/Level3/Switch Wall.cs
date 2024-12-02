using UnityEngine;

public class SwitchWall : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;

    private PlayerLevel3Mechanics player;

    private void Awake()
    {
        player = playerObject.GetComponent<PlayerLevel3Mechanics>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (player.playerState == PlayerLevel3Mechanics.State.blue)
        {
            player.SetPlayerState(PlayerLevel3Mechanics.State.orange);
        }
        else
        {
            player.SetPlayerState(PlayerLevel3Mechanics.State.blue);
        }
    }
}
