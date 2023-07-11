using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private float _changeDirectionCooldown;
    private bool _isColliding;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();

    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            HandlePlayerTargeting();
        }
        else
        {
            HandleRandomDirectionChange();
        }
    }

    private void HandlePlayerTargeting()
    {
        _targetDirection = _playerAwarenessController.DirectionToPlayer;
    }

    private void HandleRandomDirectionChange()
    {

        _changeDirectionCooldown -= Time.deltaTime;

        if (_changeDirectionCooldown <= 0)
        {
            float angleChange = _isColliding ? 180f : Random.Range(-45f, 45f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;
            
            _changeDirectionCooldown = Random.Range(1f, 3f);
        }

    }


    private void RotateTowardsTarget()
    {

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        _rigidbody.velocity = transform.up * _speed;
    }

    public void ChangeColor()
    {
        _spriteRenderer.color = new Color(255, 0, 0);
    }

    public void BlinkDamage()
    {
        StartCoroutine(DamagedCoroutine());
    }
    private IEnumerator DamagedCoroutine()
    {
        _spriteRenderer.color = new Color(255, 0, 0);
         yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = _originalColor;
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        _isColliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isColliding = true;
    }

}
