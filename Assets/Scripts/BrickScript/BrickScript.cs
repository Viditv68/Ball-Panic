using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AnimationClip clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BreakTheBrick()
    {
        animator.Play("BrickBreak");
        yield return new WaitForSeconds(clip.length);
        gameObject.SetActive(false);
    }
}
