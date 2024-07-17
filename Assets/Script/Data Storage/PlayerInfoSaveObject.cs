using CarterGames.Assets.SaveManager;
using UnityEngine;

namespace Save
{
    [CreateAssetMenu(fileName = "PlayerInfoSaveObject")]
    public class PlayerInfoSaveObject : SaveObject
    {
        public SaveValue<Vector2> lastLocation;
        public SaveValue<int> wave;
        public SaveValue<int> currentHp;
        public SaveValue<int> maxHp;

        public SaveValue<int> stamina;

        public SaveValue<int> coin;
        
    }
}