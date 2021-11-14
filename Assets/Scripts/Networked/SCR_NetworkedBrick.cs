using Mirror;
using UnityEngine;

public class SCR_NetworkedBrick : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            // Destroy the GameObject when hit with the ball
            SCR_NetworkedGameManager.instance.IncreasePlayerScore(100);
            Destroy(gameObject);
            NetworkServer.Destroy(gameObject);
        }
    }
}
