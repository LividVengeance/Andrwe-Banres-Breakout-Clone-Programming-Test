using UnityEngine;

public class SCR_KillVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball")) Destroy(other.gameObject); 
    }
}
