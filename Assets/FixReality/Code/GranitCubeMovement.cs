using UnityEngine;

public class GranitCubeMovement : MonoBehaviour
{
    private float cubePositionX;
    private static float cubePositionY;
    private float cubePositionYStart;
    private float cubePositionYEnd;
    private float cubePositionZ;
    private float tLerp;
    private float cubeOffset = 5f;
    private float rotation = -281f;
    private float unrollSpeed = 2f;

    private Vector3 scaleChange;

    public Transform playerTransform;
    public float maxDistance = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        cubePositionX = transform.position.x;
        cubePositionYStart = transform.position.y + cubeOffset;
        cubePositionY = cubePositionYStart;
        cubePositionYEnd = transform.position.y;
        cubePositionZ = transform.position.z;
        tLerp = 0f;

        scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);

        transform.position = new Vector3(cubePositionX, cubePositionY, cubePositionZ);
        transform.Rotate(-rotation, 0f, 0f, Space.World);

        transform.localScale = new Vector3(tLerp, tLerp, tLerp); 
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        movementFunction(manhattanDistance() < maxDistance);        
    }

    void movementFunction(bool showPath)
    {
        if (showPath && tLerp < 0.99f)
        {
            tLerp += unrollSpeed * Time.deltaTime;
            cubePositionY = Mathf.Lerp(cubePositionYStart, cubePositionYEnd, tLerp);
            transform.Rotate(rotation * unrollSpeed * Time.deltaTime, 0f, 0f, Space.World);
            transform.position = new Vector3(cubePositionX, cubePositionY, cubePositionZ);
            transform.localScale = abs(new Vector3(tLerp, tLerp, tLerp));
        }
        else if (!showPath && tLerp > 0f)
        {
            tLerp -= unrollSpeed * Time.deltaTime;
            cubePositionY = Mathf.Lerp(cubePositionYStart, cubePositionYEnd, tLerp);
            transform.Rotate(-rotation * unrollSpeed * Time.deltaTime, 0f, 0f, Space.World);
            transform.position = new Vector3(cubePositionX, cubePositionY, cubePositionZ);
            transform.localScale = abs(new Vector3(tLerp, tLerp, tLerp));
        }
    }

    private float manhattanDistance()
    {
        var a = Mathf.Abs(playerTransform.transform.position.x - cubePositionX);
        var b = Mathf.Abs(playerTransform.transform.position.y - cubePositionYEnd);
        var c = Mathf.Abs(playerTransform.transform.position.z - cubePositionZ);
        return a + b + c;
    }

    private Vector3 abs(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }
}
