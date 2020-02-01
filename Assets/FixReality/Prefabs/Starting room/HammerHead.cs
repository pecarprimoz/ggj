using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHead : MonoBehaviour
{
    Vector3 velocity = Vector3.zero;
    bool first = true;
    Vector3 last_pos = Vector3.zero;
    // Start is called before the first frame update

    void Update()
    {
        if (first)
        {
            first = false;
              last_pos = transform.position;
            return;
        }
        velocity -= velocity/ 50;

        velocity += transform.position-last_pos;

        last_pos = transform.position;
        Debug.Log(velocity.ToString());

    }
    public bool CanNailIt(Vector3 direction)
    {
        Vector3 vV = velocity;
        Vector3 vDNorm = direction.normalized;
        float dot = Vector3.Dot(vV, direction);
        Vector3 vP = vDNorm * dot;
        if (vP.magnitude> velocity_cap_)
        {
            Debug.Log("CanNailIt true");
            return true;
        }
        Debug.LogFormat("CanNailt false mag {0} vel {1}", vP.magnitude, velocity);
        return false;
    }
    public float velocity_cap_ = 0.1f;
}
