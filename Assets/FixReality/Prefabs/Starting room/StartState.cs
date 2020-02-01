using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : MonoBehaviour
{
    struct WallAnim
    {
       public Animator anim_;
        public Timer timer_;
    }
    public Rigidbody picture_;
    Timer timer_start_;
    public Nail nail_;
    bool played_ = false;
    public List< Animator> anims_;
    List<WallAnim> wall_anims_ = new List< WallAnim>();
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
            picture_.useGravity = true;
        }
        if (nail_.state_ == Nail.NailState.kIn && !played_)
        {
            int k = 0;
            foreach(var anim in anims_)
            {
                k++;
                WallAnim wa = new WallAnim();
                wa.timer_ = new Timer(0.1f*k);
                wa.anim_ = anim;
                wall_anims_.Add(wa);
            }
            played_ = true;
        }
        foreach(var anim in wall_anims_)
        {
            if(anim.timer_.IsTime())
            {
                anim.anim_.enabled = true;
                wall_anims_.Remove(anim);
                break;
            }
        }

    }
}
