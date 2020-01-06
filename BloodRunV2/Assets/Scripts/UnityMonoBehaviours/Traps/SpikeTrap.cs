using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Animator Animator;

    private bool IsActivated;

    void Update()
    {
        if(IsActivated)
        {
            Animator.SetBool("Active", true);
        }
        else
        {
            Animator.SetBool("Active", false);
        }
    }

    public void Activate()
    {
        IsActivated = true;
    }

    public void DeActivate()
    {
        IsActivated = false;
    }
}
