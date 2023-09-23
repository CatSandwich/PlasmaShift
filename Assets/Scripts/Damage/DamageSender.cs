using System;
using UnityEngine;

namespace Damage
{
    public class DamageSender : MonoBehaviour
    {
        public float Damage;

        public void SendDamage(GameObject target)
        {
            if (target.TryGetComponent(out DamageReceiver receiver))
            {
                receiver.DamageReceived.Invoke(Damage);
            }
        }
    }
}
