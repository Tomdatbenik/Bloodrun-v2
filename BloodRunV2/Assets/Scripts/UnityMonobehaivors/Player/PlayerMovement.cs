using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public int speed;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed));

        ConnectionManager.playerLogic.MovePlayerOnServer(CreatePlayerInfoFromTransform(rb.transform));
    }

    private PlayerInfo CreatePlayerInfoFromTransform(Transform transform)
    {
        PlayerInfo player = new PlayerInfo();

        player.username = ConnectionManager.Username;

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
