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
    private float _changeDirectionCooldown;
    private bool _isColliding;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        //if (_playerAwarenessController.AwareOfPlayer)
        //{
        _targetDirection = _playerAwarenessController.DirectionToPlayer;
        //}
    }

    private void HandleRandomDirectionChange()
    {
        // if(_isColliding)
        // {
        //     _changeDirectionCooldown = 0f;
        // }
        // else 
        // {
        //     _changeDirectionCooldown -= Time.deltaTime;
        // }
        // if (Physics.Linecast(transform.position, _targetDirection))
        // {
        //     _changeDirectionCooldown = 0f;
        //     Debug.Log("colidiu");
        // }
        // else
        // {

        //     Debug.Log("nao colidiu");
        // }

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

    private void OnCollisionExit2D(Collision2D collider)
    {
        _isColliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isColliding = true;
    }

}
