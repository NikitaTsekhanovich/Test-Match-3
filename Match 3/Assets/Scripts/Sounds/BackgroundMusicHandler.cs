using UnityEngine;

namespace Sounds
{
    public class BackgroundMusicHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _BGMusic;
        private GameObject[] _gmObjects;
        private AudioSource _audioSource;

        private void Awake()
        {
            _gmObjects = GameObject.FindGameObjectsWithTag("BackgroundMusic");

            if (_gmObjects.Length == 0)
            {
                _BGMusic = Instantiate(_BGMusic);
                _BGMusic.name = "BGMusic";
                DontDestroyOnLoad(_BGMusic.gameObject);
            }
            else
            {
                _BGMusic = GameObject.FindGameObjectWithTag("BackgroundMusic");
            }
        }
        
        private void Start()
        {
            _audioSource = _BGMusic.GetComponent<AudioSource>();
        }

        public void StartMusic()
        {
            _audioSource.Play();
        }

        public void StopMusic()
        {
            _audioSource.Pause();
        }
    }
}
