using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SCR_NetworkedPaddle : NetworkBehaviour
{
    [SerializeField, Tooltip("How quickly the paddle will move")] private float moveSpeed;
    [SerializeField, Tooltip("The number of balls the player will start with")] private int numberOfBalls;
    [SerializeField, Tooltip("The prefab for the wall that will be spawned")] private GameObject ballPrefab;
    [SerializeField, Tooltip("Location of the ball when spawned in")] private Transform ballSpawnLocation;

    [SerializeField, Tooltip("The amount of force applied to the ball on releasing the ball")]
    private float releaseForce = 50;
    
    [SerializeField] private GameObject currentBall;
    private Rigidbody rb;
    private float input;
    private int currentNumOfBalls;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentNumOfBalls = numberOfBalls;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        // Get the move input
        input = Input.GetAxisRaw("Horizontal");
        
        // Release the ball when space pressed
        if (Input.GetKeyDown(KeyCode.Space) && currentBall == null) CmdCreateNewBall();
    }
    
    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        
        // Apply the move input to the paddle
        rb.velocity = (Vector3.right * input * moveSpeed);
    }
    
    [Command]
    private void CmdCreateNewBall()
    {
        if (currentNumOfBalls > 0)
        {
            currentNumOfBalls--;
            currentBall = (GameObject)Instantiate(ballPrefab, ballSpawnLocation.position, Quaternion.identity);
            NetworkServer.Spawn(currentBall);
            ShootBall();
        }
    }
    
    private void ShootBall()
    {
        // There is no ball to shoot
        if (currentBall == null && currentNumOfBalls <= 0) return;
        
        Rigidbody ballRb = currentBall.GetComponent<Rigidbody>();
        ballRb.isKinematic = false;

        // Gets random direction within 90 degrees and applies force
        float rand = Random.Range(-45f, 45f);
        Vector3 dir = Quaternion.Euler(0, 0, rand) * Vector3.one * releaseForce;
        ballRb.AddForce(dir, ForceMode.Impulse);
    }
}