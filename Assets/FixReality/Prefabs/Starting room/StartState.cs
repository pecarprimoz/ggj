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
    Timer timer_start_ = new Timer(0);
    public Nail nail_;
    bool played_ = false;
    public List<Anim> anims_;
    List<List<WallAnim>> wall_anims_ = new List<List< WallAnim>>();
    Timer timer_end_ = new Timer(0);
    public List<GameObject>disable_on_end_;
    public GameObject camera_picture_;
    // Start is called before the first frame update
    void Start()
    {
        timer_start_ = new Timer(2);
        camera_picture_.transform.position= new Vector3(camera_picture_.transform.position.x, Valve.VR.InteractionSystem.Player.instance.eyeHeight, camera_picture_.transform.position.z);


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
        if (nail_.state_ == NailState.kIn && !played_)
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
            timer_end_ = new Timer(5.0f);

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

        if (timer_end_.IsTime())
        {
            foreach (var dis in disable_on_end_)
            {
                dis.SetActive(false);
            }
        }
    }
}
