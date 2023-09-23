using System;
using UnityEngine;

namespace Damage
{
    public class DamageSender : MonoBehaviour
    {
        public float Damage;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out DamageReceiver receiver))
            {
                receiver.DamageReceived.Invoke(Damage);
            }
        }

        private void OnValidate()
        {
            if (!TryGetComponent(out Collider2D collider))
            {
                Debug.LogWarning("Damage Sender has no collider. It won't send any damage.");
            }
        }
    }
}
