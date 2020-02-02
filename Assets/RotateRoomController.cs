using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoomController : MonoBehaviour
{
    public Collider trashCollider;
    public bool win_condition_ = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().Equals(trashCollider))
        {
            win_condition_ = true;
            //Debug.Log("WIN");
        }
    }
}
