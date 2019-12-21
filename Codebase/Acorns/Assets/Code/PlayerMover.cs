using UnityEngine;

namespace Code
{
    public class PlayerMover : MonoBehaviour
    {
        private Rigidbody k_rigidBody;
    
        // Start is called before the first frame update
        void Start()
        {
        
            k_rigidBody = GetComponent<Rigidbody>();
        
            k_rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
                k_rigidBody.velocity += transform.forward;
            if (Input.GetKey(KeyCode.S))
                k_rigidBody.velocity -= transform.forward;

            if (Input.GetKey(KeyCode.A))
                k_rigidBody.velocity -= transform.right;
            if (Input.GetKey(KeyCode.D))
                k_rigidBody.velocity += transform.right;
        }
    }
}
