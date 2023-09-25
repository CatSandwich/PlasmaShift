using System.Collections;
using Damage;
using UnityEngine;

namespace Player
{
    public class Shoot : MonoBehaviour
    {
        public RaycastDamageSender Sender;
        public Material shootMaterial;
        public float shootAnimationTime = 0.4f; //This should probably be constant - Evan
        public const float SHOOT_RECHARGE_TIME = 1f;

        //AUDIO
        public AudioSource shootSource;
        public AudioClip[] shootClips;
        public AudioSource rechargeSource;
        public AudioClip[] rechargeClips;
        private const double RECHARGE_OFFSET = 0.72925d; //No time for audio metadata

        float lastShootTime = 0;
        bool duringAnimation = false;
        
        IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitUntil(() => Input.GetMouseButton(0));

                //We are now shooting
                lastShootTime = Time.time;
                duringAnimation = true;
                ScreenShake.StartShake();
				Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                //Audio
                if (shootSource != null && shootClips.Length > 0) //serialized arrays won't be null.
                {
                    shootSource.clip = shootClips[Random.Range(0, shootClips.Length)];
                    shootSource.Play(); 
                }
                if (rechargeSource != null && rechargeClips.Length > 0)
				{
                    rechargeSource.clip = rechargeClips[Random.Range(0, rechargeClips.Length)];
                    rechargeSource.PlayScheduled(AudioSettings.dspTime + SHOOT_RECHARGE_TIME - RECHARGE_OFFSET);
				}
                
                //Raycast
                Sender.Raycast(click - transform.position);
                yield return new WaitForSeconds(SHOOT_RECHARGE_TIME);

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
