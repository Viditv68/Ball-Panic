using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private float arrowSpeed = 6.0f;

    private bool canShootStickyArrow;

    [SerializeField]
    private AudioClip clip;
    private void Start()
    {
        canShootStickyArrow = true;
    }

    private void Update()
    {
        if(this.gameObject.tag == "FirstStickyArrow")
        {
            if(canShootStickyArrow)
            {
                ShootArrow(); ;
            }
        }
        else if(this.gameObject.tag == "SecondStickyArrow")
        {
            if(canShootStickyArrow)
            {
                ShootArrow();
            }
        }

        else
        {
            ShootArrow();
        }
    }


    void ShootArrow()
    {
        Vector3 tmp = transform.position;
        tmp.y += arrowSpeed * Time.unscaledDeltaTime;
        transform.position = tmp;
    }

    IEnumerator ResetStickyArrow()
    {
        yield return new WaitForSeconds(2.5f);

        if(this.gameObject.tag == "FirstStickyArrow")
        {
            PlayerScript.instance.PlayerShootOnce(true);
            this.gameObject.SetActive(false);
        }

        else if (this.gameObject.tag == "SecondStickyArrow")
        {
            PlayerScript.instance.PlayerShootTwice(true);
            this.gameObject.SetActive(false);
        }
    }



    private void SetTheStickyArrowPosition(GameObject target)
    {
        canShootStickyArrow = false;
        Vector3 targetPosition = target.transform.position;
        Vector3 tmp = transform.position;

        if (target.tag == "TopBrick")
        {
            targetPosition.y -= 0.7f;
        }
        else if (target.tag == "UnbreakableBrickTop" || target.tag == "UnbreakableBrickBottom" || target.tag == "UnbreakableBrickLeft" || target.tag == "UnbreakableBrickRight")
        {
            targetPosition.y -= 0.48f;
        }
        else if (target.tag == "UnbreakableBrickBottomVertical")
        {
            targetPosition.y -= 0.7f;
        }

        tmp.y = targetPosition.y;
        transform.position = tmp;

        AudioSource.PlayClipAtPoint(clip, transform.position);
        StartCoroutine("ResetStickyArrow");
    }



    private void OnTriggerEnter2D(Collider2D target)
    {

        if(target.tag == "LargestBall" || target.tag == "LargeBall" || target.tag == "MediumBall" || target.tag == "SmallBall" || target.tag == "SmallestBall")
        {
            if(gameObject.tag == "FirstArrow" || gameObject.tag == "FirstStickyArrow")
            {
                PlayerScript.instance.PlayerShootOnce(true);
            }
            else if(gameObject.tag == "SecondArrow" || gameObject.tag == "SecondStickyArrow")
            {
                PlayerScript.instance.PlayerShootTwice(true);
            }
            gameObject.SetActive(false);
        }


        if(target.tag =="TopBrick" || target.tag == "UnbreakableBrickTop" || target.tag =="UnbreakableBrickBottom" || target.tag == "UnbreakableBrickLeft" || target.tag =="UnbreakableBrickRight" || target.tag == "UnbreakableBrickBottomVertical")
        {
            if(this.gameObject.tag == "FirstArrow")
            {
                PlayerScript.instance.PlayerShootOnce(true);
                this.gameObject.SetActive(false);
            }

            else if (this.gameObject.tag == "SecondArrow")
            {
                PlayerScript.instance.PlayerShootTwice(true);
                this.gameObject.SetActive(false);
            }

            if (this.gameObject.tag == "FirstStickyArrow")
            {
                SetTheStickyArrowPosition(this.gameObject);

                
            }

            else if(this.gameObject.tag == "SecondStickyArrow")
            {
                SetTheStickyArrowPosition(this.gameObject);
            }
        }


        if(target.tag == "BrokenBrickTop" || target.tag == "BrokenBrickBottom" || target.tag == "BrokenBrickLeft" || target.tag == "BrokenBrickRight")
        {
            if(gameObject.tag == "FirstArrow" || gameObject.tag == "FirstStickyArrow")
            {
                PlayerScript.instance.PlayerShootOnce(true);
            }
            else if(gameObject.tag == "SecondArrow" || gameObject.tag == "SecondStickyArrow")
            {
                PlayerScript.instance.PlayerShootTwice(true);
            }

            gameObject.SetActive(false);

        }

       
    }
}
