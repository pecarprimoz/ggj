using UnityEngine;

namespace Assets.Resources.Portal
{
    public class RecursivePortalCamera : MonoBehaviour
    {
        [SerializeField]
        private Portal[] portals = new Portal[4];

        [SerializeField]
        private RenderTexture[] renderTextures;

        [SerializeField]
        private Camera portalCamera;

        private Camera mainCamera;

        private const int iterations = 2;

        private void Awake()
        {
            mainCamera = GetComponent<Camera>();

            renderTextures = new RenderTexture[portals.Length];

            for (var i = 0; i < portals.Length; i++)
            {
                renderTextures[i] = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
            }
        }

        private void Start()
        {
            for (var i = 0; i < portals.Length; i++)
            {
                portals[i].SetTexture(renderTextures[i]);
            }
        }

        private void OnPreRender()
        {
            for (var portalPairIndex = 0; portalPairIndex < portals.Length-1; portalPairIndex += 2)
            {
                if (!portals[portalPairIndex].IsPlaced() || !portals[portalPairIndex+1].IsPlaced())
                {
                    return;
                }

                if (portals[portalPairIndex].IsRendererVisible())
                {
                    portalCamera.targetTexture = renderTextures[portalPairIndex];
                    for (var i = iterations - 1; i >= 0; --i)
                    {
                        RenderCamera(portals[0], portals[1], i);
                    }
                }

                if (portals[portalPairIndex+1].IsRendererVisible())
                {
                    portalCamera.targetTexture = renderTextures[portalPairIndex+1];
                    for (var i = iterations - 1; i >= 0; --i)
                    {
                        RenderCamera(portals[portalPairIndex+1], portals[portalPairIndex], i);
                    }
                }
            }
        }

        private void RenderCamera(Portal inPortal, Portal outPortal, int iterationID)
        {
            Transform inTransform = inPortal.transform;
            Transform outTransform = outPortal.transform;

            Transform cameraTransform = portalCamera.transform;
            cameraTransform.position = transform.position;
            cameraTransform.rotation = transform.rotation;

            for(int i = 0; i <= iterationID; ++i)
            {
                // Position the camera behind the other portal.
                Vector3 relativePos = inTransform.InverseTransformPoint(cameraTransform.position);
                relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
                cameraTransform.position = outTransform.TransformPoint(relativePos);

                // Rotate the camera to look through the other portal.
                Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * cameraTransform.rotation;
                relativeRot = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeRot;
                cameraTransform.rotation = outTransform.rotation * relativeRot;
            }

            // Set the camera's oblique view frustum.
            Plane p = new Plane(-outTransform.forward, outTransform.position);
            Vector4 clipPlane = new Vector4(p.normal.x, p.normal.y, p.normal.z, p.distance);
            Vector4 clipPlaneCameraSpace =
                Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * clipPlane;

            var newMatrix = mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
            portalCamera.projectionMatrix = newMatrix;

            // Render the camera to its render target.
            portalCamera.Render();
        }
    }
}
