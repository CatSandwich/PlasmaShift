using UnityEngine;
using UnityEngine.Events;

namespace Damage
{
    public class DamageReceiver : MonoBehaviour
    {
        public UnityEvent<float> DamageReceived;

        public void LogDamage(float damage)
        {
            Debug.Log($"{name}: Received {damage} damage.");
        }
        
        private void OnValidate()
        {
            if (!GetComponent<Collider2D>())
            {
                Debug.LogWarning("Damage Receiver has no collider. It won't receive any damage.");
            }
        }
    }
}