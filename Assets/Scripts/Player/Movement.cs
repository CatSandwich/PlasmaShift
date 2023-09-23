using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        public Rigidbody2D Rigidbody;

        public float Acceleration;
        public float MaxSpeed;
        
        public void Update()
        {
            // Read input.
            Vector2 input = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) input += Vector2.up;
            if (Input.GetKey(KeyCode.A)) input += Vector2.left;
            if (Input.GetKey(KeyCode.S)) input += Vector2.down;
            if (Input.GetKey(KeyCode.D)) input += Vector2.right;
            input.Normalize();

            // Apply input.
            Rigidbody.velocity += input * (Acceleration * Time.deltaTime);

            // Clamp to max speed.
            if (Rigidbody.velocity.magnitude > MaxSpeed)
            {
                Rigidbody.velocity = Rigidbody.velocity.normalized * MaxSpeed;
            }
        }
    }
}
