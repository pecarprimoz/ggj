using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : Object
{
    public bool passed = true;
    float max_ = 0;
    float seconds_=0;
  public  Timer(float seconds) {
        max_ = seconds;
        passed = seconds <= 0;
    }

    // Update is called once per frame
   public bool IsTime()
    {
        float delta_time = Time.deltaTime;
        if (passed)
        {
            return false;
        }
        seconds_ += delta_time;
        if (seconds_ > max_)
        {
            return true;
        }
        return false;
    }
    public void Stop()
    {
        passed = true;
    }
}
