using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    private readonly float _speed = 3f;
    private Animator _animator;
    private Vector2 _moveDelta;
    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }

    private void FixedUpdate()
    {
        CalculateMovement();
        AnimateMovement();
    }
    

    private void AnimateMovement()
    {
        if (_moveDelta != Vector2.zero)
        {
            _animator.SetBool("moving", true);
            _animator.SetFloat("movex", _moveDelta.x);
            _animator.SetFloat("movey", _moveDelta.y);
        }
        else
        {
            _animator.SetBool("moving", false);
        }
    }

    public void CalculateMovement()
    {
        _moveDelta = _playerInput.Player.Move.ReadValue<Vector2>();
		_rigidbody.velocity = (_moveDelta * _speed);
    }

    public void DisableMovement()
    {
        if (_playerInput.Player.Move.enabled)
        {
            _playerInput.Player.Move.Disable();
        }
    }

    public void EnableMovement()
    {
        if (!_playerInput.Player.Move.enabled)
        {
            _playerInput.Player.Move.Enable();
        }
    }
}
