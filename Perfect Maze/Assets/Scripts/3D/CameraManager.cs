using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    //determines the amount of buffer space.
    private float _buffer = 2f;
    [SerializeField]
    private float _movementSpeed = 10f;
    private Bounds _bounds;

    public void CalculateOrthoSize()
    {
        //create a Bounds as a point of reference for the camera, to later use to determine the centerpoint of the maze.
        _bounds = new Bounds();
        foreach (BoxCollider col in FindObjectsOfType<BoxCollider>()) _bounds.Encapsulate(col.bounds);

        _bounds.Expand(_buffer);

        float vertical = _bounds.size.z;
        float horizontal = _bounds.size.x * GetComponent<Camera>().pixelHeight / GetComponent<Camera>().pixelWidth;
        Debug.Log(_bounds);

        float size = Mathf.Max(horizontal, vertical) * 0.5f;
        Vector3 center = _bounds.center + new Vector3(0, 10, 0);

        transform.position = center;
        GetComponent<Camera>().orthographicSize = size;
        if (GetComponent<Camera>().orthographicSize > 15)
            GetComponent<Camera>().orthographicSize = 15;
    }

    void Update()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        Vector3 movement = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        Vector3 newPosition = transform.position + movement * _movementSpeed * Time.deltaTime;

        // Ensure the camera never goes out of bounds.
        newPosition.x = Mathf.Clamp(newPosition.x, _bounds.min.x, _bounds.max.x);
        newPosition.z = Mathf.Clamp(newPosition.z, _bounds.min.z, _bounds.max.z);

        transform.position = newPosition;
    }
}
