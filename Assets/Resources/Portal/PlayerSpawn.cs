using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public BezierSpline Spline;
    public float fallDistance = 10;

    private Vector2 start;
    private Vector2 end;
    private float distanceReal;
    private float distanceHeuristic;
    private float t = -100f;

    Transform player_;

    // Start is called before the first frame update
    void Start()
    {
        player_ = Camera.main.transform;
       
    }
    bool activated = false;
    void Activate()
    {
        activated = true;
        start = new Vector2(Spline.GetPoint(0f).x, Spline.GetPoint(0f).z);
        end = new Vector2(Spline.GetPoint(1f).x, Spline.GetPoint(1f).z);
        distanceReal = Spline.GetLengthApproximately(0f, 1f, 1000f);
        distanceHeuristic = Vector2.Distance(start, end);
    }
    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            if (Vector3.Distance(transform.position, player_.position) < 500f)
            {
                Activate();
            }
            else
            {
                return;
            }
        }

        if (player_.position.y < Spline.GetPoint(0f).y - 0.5)
        {
            float distance;
            float koef;

            if (t == -100f)
            {
                distance = Vector2.Distance(new Vector2(player_.position.x, player_.position.z), end);
                koef = distance / distanceHeuristic;
                if (koef > 1f)
                    t = 1;
                else
                    t = koef;
            }
            if (player_.position.y < Spline.GetPoint(0f).y - fallDistance)
            {
                if (t > 1)
                    t = 1;
                player_.position = Spline.GetPoint(1-t) + fallDistance * Vector3.up;
                t = -100f;
            }
        }
    }
}
