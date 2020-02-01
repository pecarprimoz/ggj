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
        
    }
    private void OnTriggerEnter(Collider other)
    {
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
}