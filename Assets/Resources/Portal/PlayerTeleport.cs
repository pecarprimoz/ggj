using UnityEngine;

namespace Assets.Resources.Portal
{
    public class PlayerTeleport : MonoBehaviour
    {
        public Portal EnteredPortal { get; set; }
        public bool InsidePortal { get; set; }

        public bool InsideRoom { get; set; } = false;

        private void OnTriggerEnter(Collider collider)
        {
            var inPortal = collider.GetComponent<Portal>();
            var outPortal = inPortal.GetOtherPortal();

            if (!InsidePortal)
            {
                Debug.Log("Entered Portal");
                PlayerSpawn fall_checks = inPortal.GetComponent<PlayerSpawn>();
                if (fall_checks)
                {
                    fall_checks.enabled = false;
                }

                InsidePortal = true;
                InsideRoom = !InsideRoom;
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

              

                var graniteCubes = FindObjectsOfType<GranitCubeMovement>();
                foreach (var graniteCubeMovement in graniteCubes)
                {
                    graniteCubeMovement.PlayerInsideRoom = InsideRoom;
                }
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            var inPortal = collider.GetComponent<Portal>();

            if (inPortal == EnteredPortal)
            {
                InsidePortal = false;
                EnteredPortal = null;
            }
        }
    }
}
