using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class RandomiseScale : MonoBehaviour
{
    private MeshFilter filter;
    private MeshRenderer renderer;
    public CollectorInstance instance;
    void Start()
    {
        transform.localScale = new Vector3(Random.Range(0.1f, 0.2f), Random.Range(0.1f, 0.2f), Random.Range(0.1f, 0.2f));
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();

        filter.sharedMesh = instance.FILTERS_[Random.Range(0, instance.FILTERS_.Length - 1)] as Mesh;
        renderer.sharedMaterial = (Material)instance.RENDERERS_[Random.Range(0, instance.RENDERERS_.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {

    }

}
