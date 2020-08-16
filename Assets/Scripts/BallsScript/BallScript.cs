using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private float forceX;
    private float forceY;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private bool moveLeft, MoveRight;
    // Start is called before the first frame update

    private void Awake()
    {
        SetBallSpeed(); 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();
    }

    void MoveBall()
    {
        if(moveLeft)
        {
            Vector3 tmp = transform.position;
            tmp.x -= (forceX * Time.deltaTime);
            transform.position = tmp;
        }

        if (MoveRight)
        {
            Vector3 tmp = transform.position;
            tmp.x += (forceX * Time.deltaTime);
            transform.position = tmp;
        }
    }


    void SetBallSpeed()
    {
        forceX = 2.5f;

        switch(this.gameObject.tag)
        {
            case "LargestBall":
                forceY = 11.5f;
                break;

            case "LargeBall":
                forceY = 10.5f;
                break;

            case "MediumBall":
                forceY = 9f;
                break;

            case "SmallBall":
                forceY = 8f;
                break;

            case "SmallestBall":
                forceY = 7f;
                break;
        }


    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "BottomBrick")
        {
            myRigidBody.velocity = new Vector2(0, forceY);
        }

        if(target.tag == "LeftBrick")
        {
            moveLeft = false;
            MoveRight = true;
        }

        if (target.tag == "RightBrick")
        {
            MoveRight = false;
            moveLeft = true;
        }
    }
}
