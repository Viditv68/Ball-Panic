using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
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


    private void Awake()
    {
        float cameraHeight = Camera.main.orthographicSize;
        height = -cameraHeight - 0.8f;
        canWalk = true;
    }

    private void Update()
    {
        ShootTheArrow();
    }

    private void FixedUpdate()
    {
        PlayerWalkKeyboard();
    }

    public void ShootTheArrow()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PlayTheSHootAnimation());
            Instantiate(arrows[0], new Vector3(transform.position.x, height, 0), Quaternion.identity);

        }
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
