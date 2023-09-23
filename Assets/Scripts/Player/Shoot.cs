using System.Collections;
using Damage;
using UnityEngine;

namespace Player
{
    public class Shoot : MonoBehaviour
    {
        public RaycastDamageSender Sender;
        
        IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Sender.Raycast(click - transform.position);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
