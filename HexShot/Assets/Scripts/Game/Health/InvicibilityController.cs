using System.Collections;
using UnityEngine;

public class InvicibilityController : MonoBehaviour
{
    private HealthController _healthController;
    [SerializeField]
    private float _blinkInterval;

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    private void Awake()
    {
        
        _healthController = GetComponent<HealthController>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void StartInvicibility(float invicibilityDuration)
    {
        StartCoroutine(InvicibilityCoroutine(invicibilityDuration));
        StartCoroutine(BlinkCoroutine(invicibilityDuration));
    }

    private IEnumerator InvicibilityCoroutine(float invicibilityDuration)
    {
        _healthController.IsInvicibible = true;

        yield return new WaitForSeconds(invicibilityDuration);

        _healthController.IsInvicibible = false;
    }


     private IEnumerator BlinkCoroutine(float invicibilityDuration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < invicibilityDuration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(_blinkInterval);
            elapsedTime += _blinkInterval;
        }
        _spriteRenderer.enabled = true;  // Garante que o sprite esteja ativado no final do piscar
    }
}
