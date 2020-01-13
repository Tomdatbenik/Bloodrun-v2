using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushObject : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="OtherPlayer")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            rb.AddForce(other.gameObject.transform.position + player.transform.position * 100f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "OtherPlayer" || other.tag == "Player") 
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward * 2000f);
        }
    }
}
