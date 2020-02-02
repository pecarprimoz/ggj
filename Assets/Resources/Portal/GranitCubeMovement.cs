using UnityEngine;

namespace Assets.Resources.Portal
{
    public class GranitCubeMovement : MonoBehaviour
    {

        public GameObject renderer;
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

        public bool PlayerInsideRoom;

        public float maxDistance = 10f;
    
        public Transform transform_to_move_;
        // Start is called before the first frame update
        void Start()
        {
            cubePositionX = transform_to_move_.localPosition.x;
            cubePositionYStart = transform_to_move_.localPosition.y + cubeOffset;
            cubePositionY = cubePositionYStart;
            cubePositionYEnd = transform_to_move_.localPosition.y;
            cubePositionZ = transform_to_move_.localPosition.z;
            tLerp = 0f;

          
            if(Valve.VR.InteractionSystem.Player.instance != null)
            {
                playerTransform = Valve.VR.InteractionSystem.Player.instance.transform;
            }

            scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);

            transform_to_move_.localPosition = new Vector3(cubePositionX, cubePositionY, cubePositionZ);
            transform_to_move_.Rotate(-rotation, 0f, 0f, Space.World);

            transform_to_move_.localScale = new Vector3(tLerp, tLerp, tLerp);

            renderer.SetActive(false);
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            if (PlayerInsideRoom)
            {
                return;
            }
            renderer.SetActive(true);
            movementFunction(manhattanDistance() < maxDistance);        
        }

        void movementFunction(bool showPath)
        {
            if (showPath && tLerp < 0.99f)
            {
                tLerp += unrollSpeed * Time.deltaTime;
                cubePositionY = Mathf.Lerp(cubePositionYStart, cubePositionYEnd, tLerp);
                    transform_to_move_.Rotate(rotation * unrollSpeed * Time.deltaTime, 0f, 0f, Space.World);
                transform_to_move_.localPosition = new Vector3(cubePositionX, cubePositionY, cubePositionZ);
                transform_to_move_.localScale = abs(new Vector3(tLerp, tLerp, tLerp));
            }
            else if (!showPath && tLerp > 0f)
            {
                tLerp -= unrollSpeed * Time.deltaTime;
                cubePositionY = Mathf.Lerp(cubePositionYStart, cubePositionYEnd, tLerp);
                     transform_to_move_.Rotate(-rotation * unrollSpeed * Time.deltaTime, 0f, 0f, Space.World);
                transform_to_move_.localPosition = new Vector3(cubePositionX, cubePositionY, cubePositionZ);
                transform_to_move_.localScale = abs(new Vector3(tLerp, tLerp, tLerp));
            }
        }

        private float manhattanDistance()
        {
            var a = Mathf.Abs(playerTransform.transform.position.x - transform.position.x);
            var b = Mathf.Abs(playerTransform.transform.position.y - transform.position.y);
            var c = Mathf.Abs(playerTransform.transform.position.z - transform.position.z);
            return a + b + c;
        }

        private Vector3 abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }
    }
}
