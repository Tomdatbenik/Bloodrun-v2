using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public int speed;
    public Animator Animator;
    public GameObject PlayerRotationSameAs;
    public Attacking attacking;

    private int initspeed;
    private int pushingspeed;


    private void Start()
    {
        initspeed = speed;
        pushingspeed = speed / 2;
    }


    private void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Attack()
    {
        if(Input.GetMouseButton(0))
        {
            attacking.attacking = true;
            speed = pushingspeed;
        }
        else
        {
            attacking.attacking = false;
            speed = initspeed;
        }
        ConnectionManager.playerLogic.PlayerPushing(CreatePlayerPushing(attacking.attacking));
    }

    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Animator.SetBool("Running", true);
        }
        else
        {
            Animator.SetBool("Running", false);
        }

        if(Input.GetAxisRaw("Vertical") > 0)
        {
            Animator.SetFloat("Vertical", 1);
        }
        else
        {
            Animator.SetFloat("Vertical", 0);
        }

        rb.MovePosition(rb.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed));

        ConnectionManager.playerLogic.MovePlayerOnServer(CreatePlayerInfoFromTransform(rb.transform));
    }

    private void Rotate()
    {
        Vector3 CamCenter = Camera.main.transform.position;
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

        PlayerRotationSameAs.transform.position = new Vector3(CamCenter.x, mousePos.origin.y, CamCenter.z);
        PlayerRotationSameAs.transform.LookAt(mousePos.origin);

        Quaternion rotation = PlayerRotationSameAs.transform.rotation;

        transform.rotation = rotation;
    }

    private PlayerInfo CreatePlayerInfoFromTransform(Transform transform)
    {
        PlayerInfo player = new PlayerInfo();

        player.username = ConnectionManager.CurrentUsername;

        player.transform.location.x = transform.position.x.ToString();
        player.transform.location.y = transform.position.y.ToString();
        player.transform.location.z = transform.position.z.ToString();


        player.transform.rotation.x = gameObject.transform.rotation.x.ToString();
        player.transform.rotation.y = gameObject.transform.rotation.y.ToString();
        player.transform.rotation.z = gameObject.transform.rotation.z.ToString();
        player.transform.rotation.w = gameObject.transform.rotation.w.ToString();

        return player;
    }

    private PlayerInfo CreatePlayerPushing(bool pushing)
    {
        PlayerInfo player = new PlayerInfo();

        player.username = ConnectionManager.CurrentUsername;

        player.pushing = pushing;

        return player;
    }

    private PlayerBodyData GetPlayerBodyData(GameObject gameObject)
    {
        return gameObject.GetComponent(typeof(PlayerBodyData)) as PlayerBodyData;
    }

}
