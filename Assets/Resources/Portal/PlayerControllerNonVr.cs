using UnityEngine;

namespace Assets.Resources.Portal
{
    public class PlayerControllerNonVr : MonoBehaviour
    {
        public float MovementSpeed;

        public bool InsidePortal;
        public Portal EnteredPortal;
        bool active = false;
        private void Update()
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                active = true;
                var list=    gameObject.GetComponentsInChildren<Collider>();
                foreach(var col in list)
                {
                    col.enabled = false;
                }
            }
            if(!active)
            {
                return;
            }
            var movement = transform.right * Input.GetAxisRaw("Horizontal") +
                           transform.forward * Input.GetAxisRaw("Vertical");
            movement = movement.normalized * MovementSpeed * Time.deltaTime;

            transform.position += movement;

            transform.eulerAngles += Vector3.up * Input.GetAxisRaw("Mouse X");
            Camera.main.transform.localEulerAngles += Vector3.right * Input.GetAxisRaw("Mouse Y") * -1;
        }
    }
}
