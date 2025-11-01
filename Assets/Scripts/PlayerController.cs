using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    private bool isGameOver = false;

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
        // Create a vector2 variable nad store the x and y movement values in it
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
        WinGame();        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            FindAnyObjectByType<GameManager>().EngGame();
        }
    }

    void WinGame()
    {
        if (score >= 10)
        {
            winTextObject.SetActive(true);
            FindAnyObjectByType<GameManager>().CompleteLevel();       
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                // Get the TextMeshProUGUI component from the winTextObject
                TextMeshProUGUI winText = winTextObject.GetComponent<TextMeshProUGUI>();
                // Aplicated styles to the win text
                winText.text = "<color=#FFD700><b><i>CONGRATULATIONS! YOU FINISHED THE GAME!</i></b></color>";
                winText.fontStyle = FontStyles.Bold | FontStyles.Italic;
                // Destroy all enemies in the scene
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            }
        }
    }

    void EndGame()
    {
        if (!isGameOver && rb.position.y < -1f)
        {
            isGameOver = true;
            Destroy(gameObject);
            FindAnyObjectByType<GameManager>().EngGame();
        }
    }
}