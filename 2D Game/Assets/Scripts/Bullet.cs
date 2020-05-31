using System;
using UnityEngine;

namespace DefaultNamespace
{

    public class Bullet : MonoBehaviour
    {
        private void Start()
        {
            //уничтожить пулю если она летит слишком долго
            Destroy(gameObject,3f);
        }

        public int Damage = 0;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Player>()!=null)
            {
                return;
            }
            
            var hitbox = other.transform.root.GetComponentInChildren<IHitbox>();
            if (hitbox != null)
            {
                hitbox?.Hit(Damage);
            }
            
            if (!other.isTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}