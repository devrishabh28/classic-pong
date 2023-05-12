using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BallMovement : MonoBehaviour
{

    private Rigidbody2D RB;

    [SerializeField] private GameData gameData;
    [SerializeField] private int ballDirection = 1;

    [SerializeField] private float launchBallSpeed = 8;
    [SerializeField] private float maxBallSpeed = 14;

    [SerializeField] private int ballOffsetRange = 4;
    [SerializeField] private int reflectionNoise = 3;

    [SerializeField] private TMP_Text scoreTextPlayer1;
    [SerializeField] private TMP_Text scoreTextPlayer2;
    [SerializeField] private TMP_Text winText;

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private AudioClip strike;
    [SerializeField] private AudioClip bounce;

    private AudioSource source;

    private int scorePlayer1 = 0;

    private int scorePlayer2 = 0;

    private int winner = 0;

    private Vector2 velocityBall;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        source = gameObject.AddComponent<AudioSource>();
        source.pitch = 1;
        source.volume = 0.8f;
        source.loop = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        resetBall();

        scoreTextPlayer1.text = scorePlayer1.ToString();
        scoreTextPlayer2.text = scorePlayer2.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        velocityBall = RB.velocity;
    }


    private bool checkWinner()
    {
        if (scorePlayer1 == gameData.goal)
        {
            winner = 1;
            winText.text = "Player 1 Wins";
            gameOver();
            return true;
        }
        else if (scorePlayer2 == gameData.goal)
        {
            winner = 2;
            winText.text = "Player 2 Wins";
            gameOver();
            return true;
        }

        return false;

    }

    private void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void resetBall()
    {
        float y_offset_position = Random.Range(-ballOffsetRange, ballOffsetRange + 1);
        float y_offset_velocity = Random.Range(-launchBallSpeed, launchBallSpeed + 1);


        transform.position = new Vector2(0, y_offset_position);
        RB.velocity = new Vector2(ballDirection * launchBallSpeed, y_offset_velocity).normalized * launchBallSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Collided with " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            source.clip = strike;
            source.Play();
        }
        else
        {
            source.clip = bounce;
            source.Play();
        }

        Vector2 inDirection = velocityBall;
        Vector2 inNormal = collision.contacts[0].normal;
        Vector2 newVelocity = Vector2.Reflect(inDirection.normalized, inNormal) * maxBallSpeed;

        int noise = Random.Range(-reflectionNoise, reflectionNoise + 1);

        newVelocity = new Vector2(newVelocity.x, newVelocity.y + noise).normalized * maxBallSpeed;

        RB.velocity = newVelocity;

    }

    public void score(int score)
    {
        if (transform.position.x < 0)
        {
            scorePlayer2 += score;
            scoreTextPlayer2.text = scorePlayer2.ToString();
            ballDirection = 1;
            Debug.Log("Player2 scored: " + score);
        }
        else
        {
            scorePlayer1 += score;
            scoreTextPlayer1.text = scorePlayer1.ToString();
            ballDirection = -1;
            Debug.Log("Player1 scored: " + score);
        }

        if (!checkWinner())
            resetBall();
        else
        {
            RB.velocity = Vector2.zero;
        }
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return RB;
    }

}
