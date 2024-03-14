using UnityEngine;

namespace Sounds
{
    public class EffectsSoundHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void PlaySound()
        {
            if (!EffectsSoundSettings.OffSound)
            {
                _audioSource.Play();
            }
        }
    }
}
