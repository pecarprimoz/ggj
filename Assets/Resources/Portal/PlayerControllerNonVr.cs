using UnityEngine;

namespace Assets.Resources.Portal
{
    public class PlayerControllerNonVr : MonoBehaviour
    {
        public float MovementSpeed;

        public bool InsidePortal;
        public Portal EnteredPortal;

        private void Update()
        {
            var movement = transform.right * Input.GetAxisRaw("Horizontal") +
                           transform.forward * Input.GetAxisRaw("Vertical");
            movement = movement.normalized * MovementSpeed * Time.deltaTime;

            transform.position += movement;

            transform.eulerAngles += Vector3.up * Input.GetAxisRaw("Mouse X");
            Camera.main.transform.localEulerAngles += Vector3.right * Input.GetAxisRaw("Mouse Y") * -1;
        }

        private void OnTriggerEnter(Collider collider)
        {
            

            var inPortal = collider.GetComponent<Portal>();
            var outPortal = inPortal.GetOtherPortal();

            if (!InsidePortal)
            {
                Debug.Log("Entered Portal");

                InsidePortal = true;
                EnteredPortal = outPortal;

                var inTransform = inPortal.transform;
                var outTransform = outPortal.transform;

                // Update position of clone.
                Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
                relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
                transform.position = outTransform.TransformPoint(relativePos);

                // Update rotation of clone.
                Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
                relativeRot = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeRot;
                transform.rotation = outTransform.rotation * relativeRot;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            var inPortal = collider.GetComponent<Portal>();

            if (inPortal == EnteredPortal)
            {
                Debug.Log("Exited Portal");

                InsidePortal = false;
                EnteredPortal = null;
            }
        }
    }
}
