using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject player;
    public GameObject Explosion;
    public GameObject Spawnpoint;
    public GameObject deadposition;
    public bool died;
    public bool Enabled = true;

    private void Start()
    {
        died = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trap" && deadposition == null && Enabled)
        {
            deadposition = player;
            died = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Trap" && deadposition == null && Enabled)
        {
            deadposition = player;
            died = true;
        }
    }

    private void Update()
    {
        if(player.transform.position.y < -3)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            player.transform.position = Spawnpoint.transform.position + new Vector3(0f, 1f, 0f);
        }
        if(died == true)
        {
            Instantiate(Explosion, deadposition.transform.position, deadposition.transform.rotation);
            player.transform.position = Spawnpoint.transform.position + new Vector3(0f, 1f, 0f);
            deadposition = null;
            died = false;
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
        }
    }
}
