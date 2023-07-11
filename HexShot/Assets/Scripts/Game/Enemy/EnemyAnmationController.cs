using UnityEngine;

public class EnemyAnmationController : MonoBehaviour
{
    [SerializeField]
    private Animator _enemyAnimator;

    public void PlayAnimation(string animationName)
    {
        _enemyAnimator.Play(animationName);
    }
}
