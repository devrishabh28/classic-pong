using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D RB;
    [SerializeField] private GameData gameData;
    [SerializeField] private PlayerData data;
    [SerializeField] private BallMovement ball;

    [SerializeField] private KeyCode moveUp;
    [SerializeField] private KeyCode moveDown;

    private Vector2 playerVelocity;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerVelocity = RB.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = RB.velocity;

        if (data.AI & gameData.AI)
            aiMove();
        else
        {
            playerMove();
        }
    }

    private void aiMove()
    {

        // transform.position = new Vector2(transform.position.x, ball.transform.position.y);

        float ballVelocity = ball.GetRigidbody2D().velocity.y;
        ballVelocity = Mathf.Clamp(ballVelocity, ballVelocity, data.moveSpeed);

        float moveTowards = Mathf.MoveTowards(transform.position.y, ball.transform.position.y, data.moveSpeed * Time.deltaTime);

        if (isBallIncoming())
            transform.position = new Vector3(transform.position.x, moveTowards, transform.position.z);

    }

    private void playerMove()
    {
        if (gameData.mouse && gameData.AI)
        {
            Vector3 mouseInput = Input.mousePosition;
            mouseInput.z = Camera.main.nearClipPlane + 1;

            mouseInput = Camera.main.ScreenToWorldPoint(mouseInput);
            float mouseInputY = mouseInput.y;
            mouseInputY = Mathf.Clamp(mouseInputY, -7.75f, 7.75f);


            transform.position = new Vector2(transform.position.x, mouseInputY);
        }

        if (Input.GetKeyDown(moveUp))
        {
            RB.velocity = new Vector2(RB.velocity.x, data.moveSpeed);
        }


        if (Input.GetKeyDown(moveDown))
        {
            RB.velocity = new Vector2(RB.velocity.x, -data.moveSpeed);
        }

        if (Input.GetKeyUp(moveUp) || Input.GetKeyUp(moveDown))
        {
            RB.velocity = Vector2.zero;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RB.velocity = playerVelocity;
    }

    private bool isBallIncoming()
    {
        return Mathf.Sign(ball.GetRigidbody2D().velocity.x) == Mathf.Sign(transform.position.x);
    }
}
