using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        public Rigidbody2D Rigidbody;

        public float Acceleration;
        public float MaxSpeed;
        /// How much speed (fraction %) the player will lose over one second.
        public float SpeedFalloffFactor;
        public float ChangeDirectionFactor;
        
        public void Update()
        {
            // Read input.
            Vector2 input = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) input += Vector2.up;
            if (Input.GetKey(KeyCode.A)) input += Vector2.left;
            if (Input.GetKey(KeyCode.S)) input += Vector2.down;
            if (Input.GetKey(KeyCode.D)) input += Vector2.right;
            input.Normalize();
            
            Rigidbody.velocity *= (1 - SpeedFalloffFactor * Time.deltaTime);
            
            // Apply ChangeDirectionFactor.
            if (Mathf.Abs(Vector2.SignedAngle(Rigidbody.velocity, input)) > 90f)
            {
                input *= ChangeDirectionFactor;
            }
            
            // Apply input.
            Rigidbody.velocity += input * (Acceleration * Time.deltaTime);

            // Clamp to max speed.
            if (Rigidbody.velocity.magnitude > MaxSpeed)
            {
                Rigidbody.velocity = Rigidbody.velocity.normalized * MaxSpeed;
            }

			// Face towards the mouse
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
    }
}
