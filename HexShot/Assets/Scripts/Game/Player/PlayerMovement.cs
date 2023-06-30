using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _screenBorder;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Camera _camera;

    private Animator _animatorController;

    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _animatorController = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfMouse();
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();


        if(_movementInput == Vector2.zero)
        {
            _animatorController.SetBool("isWalking", false);
        }
        else
        {
            _animatorController.SetBool("isWalking", true);
        }
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.1f);
        
        _rigidbody.velocity =_smoothedMovementInput * _speed;

        
    }

    private void RotateInDirectionOfMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _camera.transform.position.z;

        Vector3 targetPosition = _camera.ScreenToWorldPoint(mousePosition);

        Vector3 direction = targetPosition - transform.position;

        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, direction);

        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.MoveRotation(rotation);
    }
}
