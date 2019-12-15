using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private int speed = 50;
    private int lifetime = 110;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0f, 1f * speed * Time.deltaTime, 0f);
    }

    private void FixedUpdate()
    {
        lifetime--;
        if(lifetime == 0)
        {
            Destroy(gameObject);
        }
    }
}
