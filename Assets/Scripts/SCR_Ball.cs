using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SCR_Ball : MonoBehaviour
{
    [SerializeField] private float ballSpeed;
    private Rigidbody rb;
    private Vector3 ballVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballVelocity = -transform.up * ballSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        ballVelocity = ballVelocity.normalized * ballSpeed;
        rb.velocity = ballVelocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Surface"))
        {
            for (int i = 0; i < other.contacts.Length; i++)
            {
                ballVelocity = Vector3.Reflect(ballVelocity, other.contacts[i].normal);
            }
        }
        
        // The ball hits the paddle
        if (other.gameObject.layer == LayerMask.NameToLayer("Paddle"))
        {
            // Gets the position hit on the paddle based on paddle width (-1, 1)
            float x = (other.transform.position.x - transform.position.x) / 2;
            Vector3 dir = new Vector3(x, 1, 0).normalized;
            ballVelocity = dir * 100;
        }
    }
}
