using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class RandomiseScale : MonoBehaviour
{
    private MeshFilter filter;
    private MeshRenderer renderer;
    private Rigidbody prigid_;
    public CollectorInstance instance;
    void Start()
    {
        transform.localScale = new Vector3(Random.Range(1.1f, 3.2f), Random.Range(1.1f, 3.2f), Random.Range(1.1f, 3.2f));
        transform.localRotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
        filter = GetComponentInChildren<MeshFilter>();
        renderer = GetComponentInChildren<MeshRenderer>();
        prigid_ = GetComponentInParent<Rigidbody>();
        //filter.sharedMesh = instance.FILTERS_[Random.Range(0, instance.FILTERS_.Length - 1)] as Mesh;
        renderer.sharedMaterial = (Material)instance.RENDERERS_[Random.Range(0, instance.RENDERERS_.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {
    }

}
