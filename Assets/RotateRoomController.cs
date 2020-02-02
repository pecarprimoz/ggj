using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoomController : MonoBehaviour
{
    public Collider trashCollider;
    public bool win_condition_ = false;
    public GameObject endGameObject;
    private void OnCollisionEnter(Collision collision)
    {
        if (!win_condition_ && collision.collider.Equals(trashCollider))
        {
            win_condition_ = true;
            GetComponent<BoxCollider>().isTrigger = false;
            endGameObject.SetActive(true);
        }

    }
}
