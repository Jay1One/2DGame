using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName  ="Objects/LevelObject", order=1)]
    public class LevelObjectData : ScriptableObject
    {
        public string Name = "New Level object Name";
        public int Health = 1;
        public bool isStatic;
        
    }
