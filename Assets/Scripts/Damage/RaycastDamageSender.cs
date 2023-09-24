using UnityEngine;

namespace Damage
{
    public class RaycastDamageSender : DamageSender
    {
        public LayerMask Mask;
        
        public void Raycast(Vector2 direction)
        {
            foreach (RaycastHit2D hit in Physics2D.RaycastAll(transform.position, direction, 20f, Mask))
            {
                SendDamage(hit.collider.gameObject);
            }
        }
    }
}
