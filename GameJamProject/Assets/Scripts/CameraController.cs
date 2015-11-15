using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}
