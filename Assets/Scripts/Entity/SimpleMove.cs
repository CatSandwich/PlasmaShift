using UnityEngine;

namespace Entity
{
    public class SimpleMove : MonoBehaviour
    {
        public Rigidbody2D Rigidbody;
        
        public Vector2 MinVelocity;
        public Vector2 MaxVelocity;

        private void OnEnable()
        {
            Rigidbody.velocity = new Vector2
            {
                x = Random.Range(MinVelocity.x, MaxVelocity.x),
                y = Random.Range(MinVelocity.y, MaxVelocity.y)
            };
        }
    }
}
