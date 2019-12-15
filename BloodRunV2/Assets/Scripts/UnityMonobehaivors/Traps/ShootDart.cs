using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDart : MonoBehaviour
{
    public GameObject dart;

    private bool shoot = false;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(shoot)
        {
            Instantiate(dart);
            dart.transform.position = gameObject.transform.position;
            dart.transform.rotation = gameObject.transform.rotation;
            shoot = false;
        }
    }

    public void Shoot()
    {
        shoot = true;
    }
}
