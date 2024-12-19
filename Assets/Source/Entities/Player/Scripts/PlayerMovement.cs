using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(CollisionDetector))]
public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string Jump = nameof(Jump);
    private const string Ground = nameof(Ground);

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _groundCheckDownDistance;

    private CollisionDetector _collisionDetector;
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;
    private bool _isOnLadder;
    private bool _isGrounded;

    public Rigidbody2D Rigidbody => _rigidbody;
    public bool IsOnLadder => _isOnLadder;
    public bool IsGrounded => _isGrounded;
    public float MaxMoveSpeed => _moveSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _collisionDetector = GetComponent<CollisionDetector>();
    }

    private void OnEnable()
    {
        _collisionDetector.OnTriggerEnter += ProcessLadderEnter;
        _collisionDetector.OnTriggerExit += ProcessLadderExit;
    }

    private void Update()
    {
        ProcessJump();
    }

    private void FixedUpdate()
    {
        CheckGround();
        ProcessMovement();
        ProcessLadderMovement();
        ProcessGravity();
    }

    private void ProcessLadderEnter(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ladder _))
        {
            _isOnLadder = true;
        }
    }

    private void ProcessLadderExit(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ladder _))
        {
            _isOnLadder = false;
        }
    }

    private void ProcessJump()
    {
        if (Input.GetButton(Jump) && _isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void ProcessMovement()
    {
        float horizontal = Input.GetAxis(Horizontal);

        _rigidbody.velocity = new Vector2(horizontal * _moveSpeed, _rigidbody.velocity.y);
    }

    private void ProcessLadderMovement()
    {
        if (_isOnLadder)
        {
            float vertical = Input.GetAxis(Vertical);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, vertical * _moveSpeed);
        }
    }

    private void ProcessGravity()
    {
        if (_isOnLadder)
        {
            _rigidbody.gravityScale = 0;
        }
        else
        {
            _rigidbody.gravityScale = 1;
        }
    }

    private void CheckGround()
    {
        float distance = _collider.radius - _groundCheckRadius + _groundCheckDownDistance;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _groundCheckRadius, Vector2.down, distance, LayerMask.GetMask(Ground));

        _isGrounded = hit.collider != null;
    }
}
