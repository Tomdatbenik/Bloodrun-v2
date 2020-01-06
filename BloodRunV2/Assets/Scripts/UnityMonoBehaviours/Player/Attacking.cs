using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public GameObject PlayerPush;
    public bool attacking;

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if(attacking)
        {
            PlayerPush.SetActive(true);
        }
        else
        {
            PlayerPush.SetActive(false);
        }
    }
}
