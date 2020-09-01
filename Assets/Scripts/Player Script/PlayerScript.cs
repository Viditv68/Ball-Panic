using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    private float speed = 8.0f;
    private float maxVelocity = 4.0f;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject[] arrows;

    private bool canWalk;

    [SerializeField]
    private AnimationClip clip;

    [SerializeField]
    private AudioClip shootClip;

    private float height;

    private bool shootOnce;
    private bool shootTwice;

    private bool moveLeft, moveRight;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        float cameraHeight = Camera.main.orthographicSize;
        height = -cameraHeight - 0.8f;
        canWalk = true;
        shootOnce = true;
        shootTwice = true;
    }

    private void Update()
    {
        ShootTheArrow();
    }

    private void FixedUpdate()
    {
        PlayerWalkKeyboard();
    }

    public void PlayerShootOnce(bool shootOnce)
    {
        this.shootOnce = shootOnce;
    }

    public void PlayerShootTwice(bool shootTwice)
    {
        this.shootTwice = shootTwice;
    }

    public void ShootTheArrow()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(shootOnce)
            {
                shootOnce = false;
                StartCoroutine(PlayTheSHootAnimation());
                Instantiate(arrows[2], new Vector3(transform.position.x, height, 0), Quaternion.identity);

            }
            else if(shootTwice)
            {
                shootTwice = false;
                StartCoroutine(PlayTheSHootAnimation());
                Instantiate(arrows[3], new Vector3(transform.position.x, height, 0), Quaternion.identity);
            }

        }
    }

    public void StopMoving()
    {
        moveLeft = moveRight = false;
        animator.SetBool("Walk", false);
    }


    IEnumerator PlayTheSHootAnimation()
    {
        canWalk = false;
        animator.Play("PlayerShoot");
        AudioSource.PlayClipAtPoint(shootClip, transform.position); 
        yield return new WaitForSeconds(clip.length);
        animator.SetBool("Shoot", false);
        canWalk = true;
    }

    void PlayerWalkKeyboard()
    {
        float force = 0.0f;
        float velocity = Mathf.Abs(myRigidBody.velocity.x);

        float h = Input.GetAxis("Horizontal");

        if(canWalk)
        {
            if (h > 0)
            {
                if (velocity < maxVelocity)
                {
                    force = speed;
                }

                Vector3 scale = transform.localScale;
                scale.x = 1.0f;
                transform.localScale = scale;
                animator.SetBool("Walk", true);
            }

            else if (h < 0)
            {
                if (velocity < maxVelocity)
                {
                    force = -speed;
                }

                Vector3 scale = transform.localScale;
                scale.x = -1.0f;
                transform.localScale = scale;
                animator.SetBool("Walk", true);
            }
            else if (h == 0)
            {
                animator.SetBool("Walk", false);
            }

            myRigidBody.AddForce(new Vector2(force, 0));
        }
        
    }
}
