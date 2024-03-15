using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Scene
{
    public class AnimatorLoadingScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _progressLoading;
        [SerializeField] private GameObject _loadingBlock;
        private AsyncOperation _loadingSceneOperation;
        
        public static AnimatorLoadingScreen instance;

        private void Start()
        {
            instance = this;
        }

        private void Update()
        {
            if (_loadingSceneOperation != null)
            {
                _progressLoading.text = $"{Mathf.RoundToInt(_loadingSceneOperation.progress * 100)}%";
            }
        }

        public void AnimationFade(string sceneName)
        {
            gameObject.GetComponent<Image>().raycastTarget = true;
            _loadingBlock.SetActive(true);

            DOTween.Sequence()
                .Append(gameObject.GetComponent<Image>().DOFade(1, 1f))
                .SetLink(gameObject)
                .AppendInterval(0.3f)
                .AppendCallback(OnAnimationOver);
            
            _loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
            _loadingSceneOperation.allowSceneActivation = false;
        }

        private void OnAnimationOver()
        {
            _loadingSceneOperation.allowSceneActivation = true;
        }
    }
}
