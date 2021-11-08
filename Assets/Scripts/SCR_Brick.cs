using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Brick : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            // Destroy the GameObject when hit with the ball
            SCR_GameManager.instance.IncreasePlayerScore(100);
            Destroy(gameObject);
        }
    }
}
