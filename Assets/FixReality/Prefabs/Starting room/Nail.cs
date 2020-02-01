using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Nail : MonoBehaviour
{

    public enum NailState
    {
        kIn = 0,
        k1 = 1,
        kOut =2,
    }
    public NailState state_;
    public Vector3 step_move_;

    // Update is called once per frame
    void Update()
    {
        if (state_ == NailState.kOut)
        {
            bool prev = close_to_cross_;
            close_to_cross_ = Vector3.Distance(end_.transform.position, cross_.transform.position) < distance_cross_;
            if (prev != close_to_cross_)
            {
                if (close_to_cross_)
                {
                    rend_.material = highlighted_;
                }
                else
                {
                    rend_.material = normal_;
                }
            }
        }
        if (NailState.kOut != state_)
        {
            rigidbody_.transform.rotation = Quaternion.Lerp(rigidbody_.transform.rotation, Quaternion.identity, 0.1f);
        }
        if(!time_.passed && time_.IsTime())
        {
            triggered_ = false;
        }
    }
    bool triggered_ = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered_)
        {
            return;
        }
        if (!close_to_cross_ && state_==NailState.kOut)
        {
            return;
        }
        if(state_ == NailState.kIn)
        {
            return;
        }
        if(other.gameObject.layer == 8)//collision1, sue me, its my day off
        {
            time_ = new Timer(0.4f);
            triggered_ = true;
            HammerHead hammerhead=  other.GetComponentInChildren<HammerHead>();
           if(hammerhead.CanNailIt(transform.TransformDirection(Vector3.down)))
            {

                Debug.Log("IncrementState");
                IncrementState();
            }
            
        }
    }
    Timer time_ = new Timer(0);
    void IncrementState()
    {
        switch (state_)
        {
            case NailState.kIn:
                break;
            case NailState.k1:
                ChangeState(NailState.kIn);
                break;
            case NailState.kOut:
                rend_.material = normal_;
                ChangeState(NailState.k1);
                break;
        }
       
    }
    void ChangeState(NailState new_state) 
    {
        Debug.LogFormat("Change state {0}", new_state);
        state_ = new_state;
        switch (new_state)
        {
            case NailState.kIn:

                DisableGrab();
                transform.position = transform.position + transform.rotation * step_move_;
                particle_.Play();
                break;
            case NailState.k1:
                DisableGrab();
                transform.position = transform.position + transform.rotation* step_move_; 
                particle_.Play();
                break;
            case NailState.kOut:
                break;
        }
    }
    void DisableGrab()
    {
        if (rigidbody_)
        {
            Hand left = Valve.VR.InteractionSystem.Player.instance.leftHand;
            if (left.currentAttachedObject == rigidbody_.gameObject)
            {
                left.DetachObject(rigidbody_.gameObject);
            }
            else
            {
                Hand right = Valve.VR.InteractionSystem.Player.instance.rightHand;
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
    public ParticleSystem particle_;
    public Rigidbody rigidbody_;
    public float distance_cross_ = 0.1f;
    public GameObject cross_;
    bool close_to_cross_ = false;
    public GameObject end_;
    public MeshRenderer rend_;
    public Material highlighted_;
    public Material normal_;

}