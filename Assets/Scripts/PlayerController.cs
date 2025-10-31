using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;   
    public float groundDistance = 0.4f;
    public float jumpForce = 10f;   
    private int score =0;
    public float speed = 0f;
    private Rigidbody rb;   // Holds movment x and y
    private float movementX;
    private float movementY;
    private bool isGrounded;

    //Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        SetCountText();
        
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Set the movement to the x and z variables (keep y at 0)
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
        EndGame();
    }

    // Called by the Input System when movement is performed
    private void OnMove(InputValue movementValue)
    {
        // create a vector2 variable nad store the x and y movement values in it
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Called by the Input System when jump is performed
    private void OnJump()
    {
        // Check if the player is grounded by creating a sphere at the groundCheck position with a radius of groundDistance and checking if it collides with any objects on the ground layer
        if (!isGrounded) return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        scoreText.text = score.ToString();

        if (score >= 2)
        {
            FindAnyObjectByType<GameManager>().CompleteLevel();
            winTextObject.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void EndGame()
    {
        if (rb.position.y < -1f)
        {
            FindAnyObjectByType<GameManager>().EngGame();
        }
    }

}