using UnityEngine;

namespace Entity
{
    public class Clip : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Enemy enemy))
            {
                enemy.Score /= 2;
            }
            
            Destroy(col.gameObject);
        }
    }
}
