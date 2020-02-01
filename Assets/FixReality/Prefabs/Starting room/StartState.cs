using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : MonoBehaviour
{

    public Rigidbody picture_;
    Timer timer_start_;
    // Start is called before the first frame update
    void Start()
    {
        timer_start_ = new Timer(2);
        
    }

    // Update is called once per frame
    void Update()
    {
      if(timer_start_.IsTime())
        {
            Debug.Log("Picture dropped");
            timer_start_.Stop();
           // picture_.useGravity = true;
        }
    }
}
