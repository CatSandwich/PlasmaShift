using UnityEngine;

namespace Entity
{
    public class Obstacle : MonoBehaviour
    {
        private void OnValidate()
        {
            if (!GetComponent<Collider2D>())
            {
                Debug.LogWarning($"No collider found on Obstacle {name}!");
            }
        }
    }
}
