using UnityEngine;
using DG.Tweening;

namespace ItemsEssence
{
    public class ItemAnimator : MonoBehaviour
    {
        public void AnimationDestroyItem()
        {
            DOTween.Sequence()
                .Append(transform.DOScale(0, 2))
                .AppendInterval(1f)
                .AppendCallback(A);
        }

        private void A()
        {
            Destroy(gameObject);
        }
    }
}
