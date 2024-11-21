using UnityEngine;

public class Wall : MonoBehaviour
{
    public enum State
    { 
        blue,
        orange
    }

    public State wallState;

    [SerializeField] private GameObject playerObject;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material orangeMaterial;


    private Collider thisCollider;
    private PlayerLevel3Mechanics player;
    private void Start()
    {
        thisCollider = GetComponent<Collider>();  
        player = playerObject.GetComponent<PlayerLevel3Mechanics>();
    }

    private void Update()
    {
        // sets the wall permeable if same state as player
        if (player.playerState.ToString() != wallState.ToString()) 
        {
            thisCollider.isTrigger = true;
        } else
        {
            thisCollider.isTrigger = false;
        }


        switch (wallState)
        {
            case (State.blue):
                GetComponent<Renderer>().material = blueMaterial;
                break;
            case (State.orange):
                GetComponent<Renderer>().material = orangeMaterial;
                break;
        }
    }

    // disable trigger (making wall impermeable), switch wall state, and switch player state;
    private void OnTriggerExit(Collider other)
    {
        if (wallState == State.blue) 
        {
            wallState = State.orange;
        } else
        {
            wallState = State.blue;
        }

        if (player.playerState == PlayerLevel3Mechanics.State.blue)
        {
            player.SetPlayerState(PlayerLevel3Mechanics.State.orange);
        } else
        {
            player.SetPlayerState(PlayerLevel3Mechanics.State.blue);
        }
    }
}
