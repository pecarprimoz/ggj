using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!close_to_cross_)
        {
            return;
        }
        if(state_ == NailState.kIn)
        {
            return;
        }
        if(other.gameObject.layer == 8)//collision1
        {
            HammerHead hammerhead=  other.GetComponentInChildren<HammerHead>();
           if(hammerhead.CanNailIt(transform.TransformDirection(Vector3.down)))
            {

                Debug.Log("IncrementState");
                IncrementState();
            }
            
        }
    }

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
        state_ = new_state;
        switch (new_state)
        {
            case NailState.kIn:
                if (rigidbody_)
                {
                    rigidbody_.constraints = RigidbodyConstraints.FreezeAll;
                }
                transform.position = transform.position + transform.rotation * step_move_;
                particle_.Play();
                break;
            case NailState.k1:
                if (rigidbody_)
                {
                    rigidbody_.constraints = RigidbodyConstraints.FreezeAll;
                }
                transform.position = transform.position + transform.rotation* step_move_; 
                particle_.Play();
                break;
            case NailState.kOut:
                break;
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