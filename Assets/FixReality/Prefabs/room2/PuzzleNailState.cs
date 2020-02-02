using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNailState : MonoBehaviour
{
    [System.Serializable]
    public struct NailStruct
    {
        public PuzzleNail nail_;
        public GameObject pillar_;
    }
    // Start is called before the first frame update
   public List<NailStruct> nails_;
    void SetState(NailStruct nail)
    {
        if (nail.nail_.state_ == NailState.kOut)
        {
            nail.pillar_.GetComponentInChildren<Animator>().Play("up");
        }
        else
        {
            nail.pillar_.GetComponentInChildren<Animator>().Play("down");
        }
    }
    void Start()
    {
        foreach (var nail in nails_)
        {
            SetState(nail);
                       
        }
    }
    void OnNailOut(PuzzleNail nail)
    {

        for(int i =0; i< nails_.Count; i++)
        {
            if(nail == nails_[i].nail_)
            {
                int prev = i -1;
                if (prev < 0)
                {
                    prev = nails_.Count - 1;
                }
                int next = i + 1;
                if (next == nails_.Count)
                {
                    next = 0;
                }

            
                nails_[prev].nail_.ThrowOut();
                nails_[next].nail_.ThrowOut();
                SetState(nails_[prev]);
                SetState(nails_[next]);
                break;

            }
        }
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        
   
        foreach (var nail in nails_)
        {
            if (nail.nail_.DidGoIn()) { 
            
                
                nail.pillar_.GetComponentInChildren<Animator>().Play("down");
                OnNailOut(nail.nail_);
                bool won = true;
                foreach (var nailend in nails_)
                {
                    if (nailend.nail_.state_ == NailState.kOut) { won = false; break; }
                }
                if (won)
                {
                    Won();
                }
            }
        }

    }
    private void Won()
    {
        finale_.enabled = true;
    }
    public Animator finale_;
}
