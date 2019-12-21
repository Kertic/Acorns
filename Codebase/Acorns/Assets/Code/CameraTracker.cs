using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class CameraTracker : MonoBehaviour
    {
        public GameObject objectToTrack;
        public float mouseTurnSpeed;


        private Vector3 _previousMousePosition;
        private Vector3 _currentMousePosition;
        private Vector3 _trackedObjectsLastOffset;


        private List<KeyValuePair<KeyValuePair<Vector3, Vector3>, Color>> debugLines;

        void Start()
        {
            debugLines = new List<KeyValuePair<KeyValuePair<Vector3, Vector3>, Color>>();
            _trackedObjectsLastOffset = transform.position - objectToTrack.transform.position;
        }

        void FixedUpdate()
        {
            if (true)
            {
                debugLines.Clear();
                transform.position = objectToTrack.transform.position + _trackedObjectsLastOffset;
                transform.LookAt(objectToTrack.transform);
                if (Input.GetKey(KeyCode.Mouse1))
                    CalculateMouseTurn();


                _trackedObjectsLastOffset = transform.position - objectToTrack.transform.position;
            }
        }

        private void CalculateMouseTurn()
        {
            Vector3 positionDelta = _currentMousePosition - _previousMousePosition;
            _previousMousePosition = _currentMousePosition;
            _currentMousePosition = Input.mousePosition;
            if (_currentMousePosition == _previousMousePosition) return;

            Vector3 objectPosition = objectToTrack.transform.position;
            //rotate around the vertical axis based off of x movement
            transform.RotateAround(objectPosition, Vector3.up, positionDelta.x * mouseTurnSpeed);
            //rotate around the axis formed by the camera's forward vector rotated 90 degrees where it intersects the players position
            transform.RotateAround(objectPosition,
                Vector3.Cross(transform.forward, objectPosition), positionDelta.y * mouseTurnSpeed);
        
            objectToTrack.transform.forward = new Vector3(transform.forward.x, objectToTrack.transform.forward.y, transform.forward.z);
        }

        void OnDrawGizmosSelected()
        {
            if (debugLines != null)
                for (int i = 0; i < debugLines.Count; i++)
                {
                    Gizmos.color = debugLines[i].Value;
                    Gizmos.DrawLine(debugLines[i].Key.Key, debugLines[i].Key.Value);
                }
        }
    }
}