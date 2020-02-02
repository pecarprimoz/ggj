using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PuzzleNail : MonoBehaviour
{


    public NailState state_;
    public Vector3 step_move_;

    // Update is called once per frame
    void Update()
    {

        if (!time_.passed && time_.IsTime())
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

        if (state_ == NailState.kIn)
        {
            return;
        }
        if (other.gameObject.layer == 8)//collision1, sue me, its my day off
        {
            time_ = new Timer(0.4f);
            triggered_ = true;
            HammerHead hammerhead = other.GetComponentInChildren<HammerHead>();
            if (hammerhead.CanNailIt(transform.TransformDirection(Vector3.down)))
            {

                Debug.Log("IncrementState");
                IncrementState();
            }

        }
    }
    Timer time_ = new Timer(0);
    public void ThrowOut()
    {
        if (state_ == NailState.kIn)
        {
            IncrementState();
        }
    }
    void IncrementState()
    {
        switch (state_)
        {
            case NailState.kIn:
                ChangeState(NailState.kOut);
                break;
            case NailState.kOut:
                ChangeState(NailState.kIn);
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
                transform.position = transform.position + transform.rotation * step_move_;
                //particle_.Play();
                did_out_ = true;
                break;
            case NailState.kOut:
                transform.position = transform.position - transform.rotation * step_move_;
                break;
        }
    }
   public bool DidGoIn()
    {
        bool prev = did_out_;
        did_out_ = false;
        return prev;
        
    }
    bool did_out_ = false;
    public ParticleSystem particle_;
    public float distance_cross_ = 0.1f;

}