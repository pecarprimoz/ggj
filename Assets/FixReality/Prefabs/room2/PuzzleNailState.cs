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
    void Start()
    {
        foreach (var nail in nails_ ){
            nail.nail_.nail_out_ += (in_nail)=>{ OnNailOut(in_nail); };
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
                break;

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
