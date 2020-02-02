using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoomController : MonoBehaviour
{
    public Collider trashCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().Equals(trashCollider))
        {
            Debug.Log("WIN");
        }
    }
}
