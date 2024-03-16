using UnityEngine;
using DG.Tweening;

namespace ItemsEssence
{
    public class ItemAnimator : MonoBehaviour
    {
        public void AnimationDestroyItem()
        {
            DOTween.Sequence()
                .Append(transform.DOScale(1.5f, 0.5f))
                .Append(transform.DOScale(0, 0.5f))
                .SetLink(gameObject)
                .AppendInterval(0.5f)
                .AppendCallback(KillItem);
        }

        private void KillItem()
        {
            Destroy(gameObject);
        }

        public void AnimationInstantiateItem()
        {
            DOTween.Sequence()
                .AppendInterval(0.5f)
                .Append(transform.DOScale(1, 2))
                .AppendInterval(0.5f);
        }
    }
}
