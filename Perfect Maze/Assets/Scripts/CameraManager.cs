using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private float _buffer = 1f;
    public void CalculateOrthoSize()
    {
        Bounds bounds = new Bounds();
        foreach (BoxCollider col in FindObjectsOfType<BoxCollider>()) bounds.Encapsulate(col.bounds);

        bounds.Expand(_buffer);

        float vertical = bounds.size.z;
        float horizontal = bounds.size.x * GetComponent<Camera>().pixelHeight / GetComponent<Camera>().pixelWidth;

        float size = Mathf.Max(horizontal, vertical) * 0.5f;
        Vector3 center = bounds.center + new Vector3(0, 10, -0.5f);

        transform.position = center;
        GetComponent<Camera>().orthographicSize = size;
    }
}
