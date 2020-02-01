using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public BezierSpline Spline;
    public GameObject Player;
    public float fallDistance = 20f;

    private Vector3 playerPosition;
    

    // Update is called once per frame
    void Update()
    {
        playerPosition = Player.transform.position;

        if (Player.transform.position.y < Spline.transform.position.y - fallDistance)
        {
            Player.transform.position = Spline.transform.position + fallDistance * Vector3.up;
        }
    }
}
