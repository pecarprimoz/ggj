using UnityEngine;

namespace Assets
{
    public class Portal : MonoBehaviour
    {
        public Portal PortalDestination;

        [Space]
        public Camera Camera;
        public RenderTexture RenderTexture;
        public Material RenderMaterial;

        private void Start()
        {
            var material = GetComponent<Material>();

            //Swap render texture with Destination portal.
            var mainTexture = material.mainTexture;
            material.mainTexture = PortalDestination.GetComponent<Material>().mainTexture;
            PortalDestination.GetComponent<Material>().mainTexture = mainTexture;
        }
    }
}
