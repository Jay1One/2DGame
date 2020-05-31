using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerWeapon : MonoBehaviour, IDamage
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private string axis = "Fire1";


        public GameObject Player_Bullet => weaponData.bullet;
        public int Damage => weaponData.damage;
        public string Axis => axis;

        public void SetDamage()
        {
            if (Player_Bullet == null)
            {
                var target = GetTarget();
                target?.Hit(Damage);
                return;
            }
//стрельба
            Bullet bullet = Instantiate(Player_Bullet, shootPoint.position, shootPoint.rotation).GetComponent<Bullet>();
            bullet.Damage = Damage;
                float bulletDirection= shootPoint.right.x;
                bullet.gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(bulletDirection*4f,0);
        }

        private IHitbox GetTarget()
        {
            IHitbox target = null;
            RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.right, weaponData.range);
            if (hit.collider != null)
            {
                target=hit.transform.root.GetComponent<IHitbox>();
            }
            return target;
        }
    }
}