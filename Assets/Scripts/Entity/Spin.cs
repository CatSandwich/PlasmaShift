using UnityEngine;

namespace Entity
{
    public class Spin : MonoBehaviour
    {
        /// The minimum rotation speed in degrees per second.
        public float MinRotationSpeed;
        /// The maximum rotation speed in degrees per second.
        public float MaxRotationSpeed;

        private float RotationSpeed;
    
        private void OnEnable()
        {
            RotationSpeed = Random.Range(MinRotationSpeed, MaxRotationSpeed);
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
        }
    }
}
