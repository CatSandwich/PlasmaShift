using System.Collections;
using Damage;
using UnityEngine;

namespace Player
{
    public class Shoot : MonoBehaviour
    {
        public RaycastDamageSender Sender;
        public Material shootMaterial;
        public float shootAnimationTime = 0.4f;

        float lastShootTime = 0;
        bool duringAnimation = false;
        
        IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                lastShootTime = Time.time;
                duringAnimation = true;
				Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Sender.Raycast(click - transform.position);
                yield return new WaitForSeconds(1f);
            }
        }

		private void Update()
		{   
            if (duringAnimation)
            {
				float timeDist = Time.time - lastShootTime;

				if (timeDist > shootAnimationTime)
                {
                    duringAnimation = false;
                    shootMaterial.SetFloat("_Hit", 0);
				}
                else
                {
					shootMaterial.SetFloat("_Hit", 1 - timeDist / shootAnimationTime);
				}
			}
		}
	}
}
