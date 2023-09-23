using UnityEngine;

namespace Damage
{
    public class TriggerDamageSender : DamageSender
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            SendDamage(col.gameObject);
        }

        private void OnValidate()
        {
            if (!GetComponent<Collider2D>())
            {
                Debug.LogWarning("Damage Sender has no collider. It won't send any damage.");
            }
        }
    }
}
