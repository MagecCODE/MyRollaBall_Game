using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    private int score =0;
    public float speed = 8.0f;
    private Rigidbody rb;   // Holds movment x and y
    private float movementX;
    private float movementY;

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

        if (rb.position.y < -1f)
        {
            FindAnyObjectByType<GameManager>().EngGame();
        }
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
        rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
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

        if (score >= 10)
        {
            FindAnyObjectByType<GameManager>().CompleteLevel();
            winTextObject.SetActive(true);
        }
    }

}