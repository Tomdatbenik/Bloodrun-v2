using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public int speed;
    public Animator Animator;

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        Debug.Log(Input.GetAxisRaw("Vertical"));

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Animator.SetBool("Running", true);
        }
        else
        {
            Animator.SetBool("Running", false);
        }

        rb.MovePosition(rb.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed));

        //ConnectionManager.playerLogic.MovePlayerOnServer(CreatePlayerInfoFromTransform(rb.transform));
    }

    private void Rotate()
    {
        if(Input.GetButton("RotateRight"))
        {
            rb.transform.Rotate(new Vector3(0, 4, 0));
        }
        else if (Input.GetButton("RotateLeft"))
        {
            rb.transform.Rotate(new Vector3(0, -4, 0));
        }

    }

    private PlayerInfo CreatePlayerInfoFromTransform(Transform transform)
    {
        PlayerInfo player = new PlayerInfo();

        player.username = ConnectionManager.CurrentUsername;

        player.transform.location.x = transform.position.x.ToString();
        player.transform.location.y = transform.position.y.ToString();
        player.transform.location.z = transform.position.z.ToString();

        player.transform.rotation.x = transform.rotation.x.ToString();
        player.transform.rotation.y = transform.rotation.y.ToString();
        player.transform.rotation.z = transform.rotation.z.ToString();
        player.transform.rotation.w = transform.rotation.w.ToString();

        return player;
    }
}
