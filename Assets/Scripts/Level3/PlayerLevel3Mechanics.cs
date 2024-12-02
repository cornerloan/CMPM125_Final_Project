using UnityEngine;

public class PlayerLevel3Mechanics : MonoBehaviour
{
    public enum State
    {
        blue,
        orange
    }

    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material orangeMaterial;

    public State playerState;

    public void SetPlayerState(State color)
    {
        playerState = color;
    }
}
