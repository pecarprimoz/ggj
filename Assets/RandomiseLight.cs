using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomiseLight : MonoBehaviour
{
    private Light light_;
    private float start_time_;
    void Start()
    {
        light_ = GetComponent<Light>();
        start_time_ = 0.0f;
    }
    void Update()
    {
        light_.color = Color.Lerp(light_.color, new Color(
      Random.Range(0f, 1f),
      Random.Range(0f, 1f),
      Random.Range(0f, 1f)), Time.deltaTime);
    }
}
