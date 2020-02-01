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
    }
}
