using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Objects/Weapon object", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public string weaponName = "Weapon name";
        public int damage = 1;
        public int range = 1;
        public float fireRate = 1f;
        public GameObject bullet;
        public float bulletSpeed;

    }
}