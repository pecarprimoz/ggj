using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathGenerateBezier : MonoBehaviour
{
    public BezierSpline spline;
    private static float splineStep;

    public GameObject granitGameObject;
    private float sidewayOffset = 1.73f;
    private float hightGameObject = 1f;
    private static List<Vector3> listGameObject;


    // Start is called before the first frame update
    void Start()
    {
        listGameObject = new List<Vector3>();

        var xDistance = Mathf.Floor(spline.GetLengthApproximately(0f, 1f, 1000f));
        splineStep = 1 / xDistance;

        // create first granitGameObject and its neightbours
        var tmpSplinePosition = new Vector3(Mathf.Floor(spline.GetPoint(0).x / sidewayOffset) * sidewayOffset, spline.GetPoint(0).y, Mathf.Floor(spline.GetPoint(0).z));
        Instantiate(granitGameObject, tmpSplinePosition, Quaternion.identity);
        listGameObject.Add(tmpSplinePosition);
        instantiateNeighbours(granitGameObject, tmpSplinePosition);
        
        // for each element in listGameObject finds nearest neighbour to the spline and create its neighbours (distinct)
        for (float i = splineStep; i <= 1; i += splineStep)
        {
            var minDistance = 1000f;
            Vector3 minCube = Vector3.zero;
            foreach (Vector3 cube in listGameObject)
            {
                var tmpDistance = Vector3.Distance(cube, spline.GetPoint(i));
                if (tmpDistance < minDistance)
                {
                    minDistance = tmpDistance;
                    minCube = cube;
                }
            }

            instantiateNeighbours(granitGameObject, minCube);
        }
    }

    void instantiateNeighbours(GameObject granitGameObject, Vector3 splinePosition)
    {

        List<Vector3> positions = new List<Vector3>();
        positions.Add(splinePosition + new Vector3(0, 0, -2));
        positions.Add(splinePosition + new Vector3(sidewayOffset, 0, -1));
        positions.Add(splinePosition + new Vector3(sidewayOffset, 0, 1));
        positions.Add(splinePosition + new Vector3(0, 0, 2));
        positions.Add(splinePosition + new Vector3(-sidewayOffset, 0, 1));
        positions.Add(splinePosition + new Vector3(-sidewayOffset, 0, -1));

        foreach(Vector3 position in positions)
        {
            Instantiate(granitGameObject, position, Quaternion.identity);
            listGameObject.Add(position);
        }
    }
}
