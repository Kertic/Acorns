using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public GameObject objectToTrack;


    public float offsetDistance;
    public int mouseTurnSpeed;


    private Vector3 _previousMousePosition;
    private Vector3 _currentMousePosition;

    void Start()
    {
    }

    void FixedUpdate()
    {
        transform.LookAt(objectToTrack.transform);
        if (Input.GetKey(KeyCode.Mouse1))
            CalculateMouseTurn();
        var vectortest = new Vector3(1, 2, 3);
        vectortest *= 2;
        transform.position = (-transform.position - objectToTrack.transform.position).normalized * offsetDistance;
    }

    private void CalculateMouseTurn()
    {
        Vector3 positionDelta = _currentMousePosition - _previousMousePosition;
        _previousMousePosition = _currentMousePosition;
        _currentMousePosition = Input.mousePosition;

        Vector3 objectPosition = objectToTrack.transform.position;
        Transform transform1;
        (transform1 = transform).RotateAround(objectPosition, Vector3.up, positionDelta.x * mouseTurnSpeed);
        transform.RotateAround(objectPosition,
            Vector3.Cross(transform1.forward, objectPosition), positionDelta.y * mouseTurnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
    }
}