using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : MonoBehaviour
{
    [System.Serializable]
    public struct Anim {
       public List<Animator> anim_;
    }
    struct WallAnim
    {
       public Animator anim_;
        public Timer timer_;
    }
    public Rigidbody picture_;
    Timer timer_start_;
    public Nail nail_;
    bool played_ = false;
    public List<Anim> anims_;
    List<List<WallAnim>> wall_anims_ = new List<List< WallAnim>>();
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
           
            for(int i =0; i<anims_.Count;i++)
            {
                int k = 0;
                wall_anims_.Add( new List<WallAnim>());
                Anim anims = anims_[i];
                foreach (var anim in anims.anim_)
                {
                    k++;
                    WallAnim wa = new WallAnim();
                    wa.timer_ = new Timer(0.1f * k);
                    wa.anim_ = anim;
                    wall_anims_[i].Add(wa);
                }
            }
            played_ = true;
        }
        foreach(var anims in wall_anims_)
        {
            foreach (var anim in anims)
            {
                if (anim.timer_.IsTime())
                {
                    anim.anim_.enabled = true;
                    anims.Remove(anim);
                    break;
                }
            }
        }

    }
}
