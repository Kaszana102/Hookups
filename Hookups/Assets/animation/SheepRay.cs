using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRay: MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if(Physics.Raycast(ray,2f,~0))
        {
            animator.SetBool("dancing", true);
        }
        else
        {
            animator.SetBool("dancing", false);
        }
    }
}
