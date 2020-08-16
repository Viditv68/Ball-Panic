using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private float arrowSpeed = 4.0f;

    private bool canShootStickyArrow;

    private void Start()
    {
        canShootStickyArrow = true;
    }

    private void Update()
    {
        ShootArrow();
    }


    void ShootArrow()
    {
        Vector3 tmp = transform.position;
        tmp.y += arrowSpeed * Time.unscaledDeltaTime;
        transform.position = tmp;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "TopBrick")
        {
            gameObject.SetActive(false);
        }
    }
}
