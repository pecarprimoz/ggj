using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoomController : MonoBehaviour
{
    public BoxCollider trashCollider;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == trashCollider) {
            Debug.Log("WIN");
        }
    }
}
