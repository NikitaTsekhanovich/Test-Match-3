using UnityEngine;

namespace Sounds
{
    public class EffectsSoundSettings : MonoBehaviour
    {
        public static bool OffSound { get; private set; }

        public void OnSoundEffects()
        {
            OffSound = false;
        }
        
        public void OffSoundEffects()
        {
            OffSound = true;
        }
    }
}
