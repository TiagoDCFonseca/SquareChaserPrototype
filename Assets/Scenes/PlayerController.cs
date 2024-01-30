using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // gets rigidbody to hold player rigidbody
    private Rigidbody2D playerRb;


    // gets collider to hold player collider
    private Collider2D playerCollider;

    public PhysicsMaterial2D bouncyMaterial;

    // Speed and jumpForce control variables
    private float speed = 15;
    private float jumpForce = 8;
    private float horizontalInput;

    //Control if player is Jumping
    private bool isJumping = false;

    //Player Health System
    public int maxLives = 3;
    private int currentLives;
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        currentLives = maxLives;
        UpdateLivesText();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }

    void PlayerControl()
    {

        //Player moves horizontal
        horizontalInput = Input.GetAxis("Horizontal");
        playerRb.transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);
        //Player jumps
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(JumpCoroutine());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(3);
        }
        else if (collision.gameObject.CompareTag("FastEnemy"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
        if (bouncyMaterial != null)
        {
            playerCollider.sharedMaterial = bouncyMaterial;
        }

    }

    IEnumerator JumpCoroutine()
    {
        isJumping = true;
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.4f);
        isJumping = false;
    }

    private void TakeDamage(int damage)
    {
        currentLives -= damage;
        UpdateLivesText();

        if (currentLives <= 0)
        {
            currentLives = 0;
            Destroy(gameObject);
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
           
        }
    }
    private void UpdateLivesText()
    {
        if(livesText != null)
        {
            livesText.text = "Lives = " + currentLives;
        }
    }

}


