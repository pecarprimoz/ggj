using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR.InteractionSystem;

public class RotateRoomController : MonoBehaviour
{
    public Collider trashCollider;
    public bool win_condition_ = false;
    public GameObject endGameObject;
    public GameObject catInst;
    private void OnCollisionEnter(Collision collision)
    {
        if (!win_condition_ && collision.collider.Equals(trashCollider))
        {
            win_condition_ = true;
            GetComponent<BoxCollider>().isTrigger = false;
            endGameObject.SetActive(true);
            catInst.transform.SetParent(transform);
            DisableGrab();
        }

    }
    void DisableGrab()
    {
        var rigidbody_ = catInst.GetComponent<Rigidbody>();
        if (rigidbody_)
        {
            Valve.VR.InteractionSystem.Hand left = Valve.VR.InteractionSystem.Player.instance.leftHand;
            if (left.currentAttachedObject == rigidbody_.gameObject)
            {
                left.DetachObject(rigidbody_.gameObject);
            }
            else
            {
                Valve.VR.InteractionSystem.Hand right = Valve.VR.InteractionSystem.Player.instance.rightHand;
                if (right.currentAttachedObject == rigidbody_.gameObject)
                {
                    right.DetachObject(rigidbody_.gameObject);
                }
            }
            foreach (Collider coll in rigidbody_.gameObject.GetComponentsInChildren<Collider>())
            {
                coll.gameObject.AddComponent<IgnoreHovering>();
            }

            rigidbody_.isKinematic = true;
        }
    }
}
