using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowBody : MonoBehaviour
{
    public Rigidbody rbBody;
    public Rigidbody rb;

    private void FixedUpdate()
    {
        rb.MovePosition(rbBody.position);
    }
}
