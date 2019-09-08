using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountPanel : MonoBehaviour
{
    Animator animator;
    static readonly int Animated = Animator.StringToHash("Animated");


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PulseCount()
    {
        if (animator != null)
        {
            if (!animator.GetBool(Animated))
            {
                animator.enabled = true;
                animator.SetBool(Animated, true);
            }
            else
            {
                //PulseCountEnd();
                //PulseCount();
            }

        }

    }

    public void PulseCountEnd()
    {
        animator.enabled = false;
        animator.SetBool(Animated, false);
    }

}
