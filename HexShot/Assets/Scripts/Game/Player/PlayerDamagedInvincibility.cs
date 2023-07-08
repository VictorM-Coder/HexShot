using UnityEngine;

public class PlayerDamagedInvincibility : MonoBehaviour
{
    [SerializeField]
    private float _invicibilityDuration;

    private InvicibilityController _invicibilityController;

    private void Awake()
    {
        _invicibilityController = GetComponent<InvicibilityController>(); 
    }

    public void StartInvicibility()
    {
        _invicibilityController.StartInvicibility(_invicibilityDuration);
    }
}
