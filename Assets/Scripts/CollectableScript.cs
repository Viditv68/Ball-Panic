using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        if(this.gameObject.tag != "InGameCollectable")
        {
            Invoke("DeactivateGameObject", Random.Range(2, 6));
        }
    }

    void DeactivateGameObject()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "BottomBrick")
        {
            Vector3 tmp = target.transform.position;
            tmp.y += 0.8f;

            transform.position = new Vector2(transform.position.x, tmp.y);
            myRigidbody.isKinematic = true;
        }

        if(target.tag == "Player")
        {
            if(this.gameObject.tag == "InGameCollectable")
            {
                GameController.instance.collectedItems[GameController.instance.currentLevel] = true;
                GameController.instance.Save();

                if(GameplayController.instance!=null)
                {
                    if(GameController.instance.currentLevel ==0 )
                    {
                        GameplayController.instance.playerScore += 1 * 1000;
                    }
                    else
                    {
                        GameplayController.instance.playerScore += GameController.instance.currentLevel * 1000;

                    }
                }
            }
            this.gameObject.SetActive(false);

        }
    }
}
