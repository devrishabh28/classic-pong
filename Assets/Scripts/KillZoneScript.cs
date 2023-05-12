using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneScript : MonoBehaviour
{

    [SerializeField] private BallMovement ball;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Entered kill zone: " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Ball")
        {
            ball.score(1);
        }

    }


}
