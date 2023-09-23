using UnityEngine;

namespace Damage
{
    public class RaycastDamageSender : DamageSender
    {
        public void Raycast(Vector2 direction)
        {
            foreach (RaycastHit2D hit in Physics2D.RaycastAll(transform.position, direction, 20f, gameObject.layer))
            {
                SendDamage(hit.collider.gameObject);
            }
        }
    }
}
