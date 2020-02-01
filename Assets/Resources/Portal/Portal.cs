using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Portal
{
    [RequireComponent(typeof(BoxCollider))]
    public class Portal : MonoBehaviour
    {
        [SerializeField]
        private Portal otherPortal;

        //[SerializeField]
        //private Renderer outlineRenderer;

        [SerializeField]
        private Color portalColour;

        [SerializeField]
        private LayerMask placementMask;

        private bool isPlaced = true;
        [SerializeField]
        private Collider wallCollider;

        private Material material;
        private new Renderer renderer;

        public LayerMask ShowLayer;

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            material = renderer.material;
        }

        private void Start()
        {
            PlacePortal(wallCollider, transform.position, transform.rotation);
            SetColour(portalColour);
        }

        public Portal GetOtherPortal()
        {
            return otherPortal;
        }

        public void SetColour(Color colour)
        {
            material.SetColor("_Colour", colour);
            //outlineRenderer.material.SetColor("_OutlineColour", colour);
        }

        public void SetTexture(RenderTexture tex)
        {
            material.mainTexture = tex;
        }

        public bool IsRendererVisible()
        {
            return renderer.isVisible;
        }

        public void PlacePortal(Collider wallCollider, Vector3 pos, Quaternion rot)
        {
            this.wallCollider = wallCollider;
            transform.position = pos;
            transform.rotation = rot;
            transform.position -= transform.forward * 0.001f;

            FixOverhangs();
            FixIntersects();
        }

        // Ensure the portal cannot extend past the edge of a surface.
        private void FixOverhangs()
        {
            var testPoints = new List<Vector3>
            {
                new Vector3(-1.1f,  0.0f, 0.1f),
                new Vector3( 1.1f,  0.0f, 0.1f),
                new Vector3( 0.0f, -2.1f, 0.1f),
                new Vector3( 0.0f,  2.1f, 0.1f)
            };

            var testDirs = new List<Vector3>
            {
                Vector3.right,
                -Vector3.right,
                Vector3.up,
                -Vector3.up
            };

            for(int i = 0; i < 4; ++i)
            {
                RaycastHit hit;
                Vector3 raycastPos = transform.TransformPoint(testPoints[i]);
                Vector3 raycastDir = transform.TransformDirection(testDirs[i]);

                if(Physics.CheckSphere(raycastPos, 0.05f, placementMask))
                {
                    break;
                }
                else if(Physics.Raycast(raycastPos, raycastDir, out hit, 2.1f, placementMask))
                {
                    var offset = hit.point - raycastPos;
                    transform.Translate(offset, Space.World);
                }
            }
        }

        // Ensure the portal cannot intersect a section of wall.
        private void FixIntersects()
        {
            var testDirs = new List<Vector3>
            {
                Vector3.right,
                -Vector3.right,
                Vector3.up,
                -Vector3.up
            };

            var testDists = new List<float> { 1.1f, 1.1f, 2.1f, 2.1f };

            for (int i = 0; i < 4; ++i)
            {
                RaycastHit hit;
                Vector3 raycastPos = transform.TransformPoint(0.0f, 0.0f, -0.1f);
                Vector3 raycastDir = transform.TransformDirection(testDirs[i]);

                if (Physics.Raycast(raycastPos, raycastDir, out hit, testDists[i], placementMask))
                {
                    var offset = (hit.point - raycastPos);
                    var newOffset = -raycastDir * (testDists[i] - offset.magnitude);
                    transform.Translate(newOffset, Space.World);
                }
            }
        }

        public bool IsPlaced()
        {
            return isPlaced;
        }
    }
}
