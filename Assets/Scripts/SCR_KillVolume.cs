using Mirror;
using UnityEngine;

public class SCR_KillVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            NetworkServer.Destroy(other.gameObject);
            Destroy(other.gameObject);
        } 
    }
}
