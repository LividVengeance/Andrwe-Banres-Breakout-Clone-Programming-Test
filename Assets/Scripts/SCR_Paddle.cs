using UnityEngine;
using Random = UnityEngine.Random;

public class SCR_Paddle : MonoBehaviour
{
    [SerializeField, Tooltip("How quickly the paddle will move")] private float moveSpeed;
    [SerializeField, Tooltip("The number of balls the player will start with")] private int numberOfBalls;
    [SerializeField, Tooltip("The prefab for the wall that will be spawned")] private GameObject ballPrefab;
    [SerializeField, Tooltip("Location of the ball when spawned in")] private Transform ballSpawnLocation;

    [SerializeField, Tooltip("The amount of force applied to the ball on releasing the ball")]
    private float releaseForce = 50;
    
    private GameObject currentBall;
    private Rigidbody rb;
    private float input;
    private bool ballDocked;
    private int currentNumOfBalls;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentNumOfBalls = numberOfBalls;
        CreateNewBall();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the move input
        input = Input.GetAxisRaw("Horizontal");
        
        // Release the ball when space pressed
        if (ballDocked && Input.GetKeyDown(KeyCode.Space)) ShootBall();
        
        // Load new ball to paddle when one is destroyed
        if (currentBall == null) CreateNewBall();
    }

    private void FixedUpdate()
    {
        // Apply the move input to the paddle
        rb.velocity = (Vector3.right * input * moveSpeed);
    }

    private void CreateNewBall()
    {
        if (currentNumOfBalls > 0)
        {
            currentNumOfBalls--;
            currentBall = Instantiate(ballPrefab, ballSpawnLocation.position, Quaternion.identity);
            currentBall.transform.parent = gameObject.transform;
            ballDocked = true;
        }
    }

    private void ShootBall()
    {
        // There is no ball to shoot
        if (currentBall == null && currentNumOfBalls <= 0) return;
        // There is no ball but can create one
        if (currentBall == null && currentNumOfBalls > 0) CreateNewBall();

        ballDocked = false;
        
        currentBall.transform.parent = null;
        Rigidbody ballRB = currentBall.GetComponent<Rigidbody>();
        ballRB.isKinematic = false;

        float rand = Random.Range(-45f, 40f);
        Vector3 dir = Quaternion.Euler(0, 0, rand) * Vector3.one * releaseForce;
        ballRB.AddForce(dir, ForceMode.Impulse);
    }
}