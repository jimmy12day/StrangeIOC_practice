using UnityEngine;
using Inputs = JimmySDK.JimmyInputs;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool listenToMouseClick = false;
    [SerializeField] bool canMove = false;
    [SerializeField] bool canPull = false;
    [SerializeField] bool isArrived = false;
    [SerializeField] float clampMax = 30f;

    public void ReceivedState(GameState stateMessage)
    {
        switch (stateMessage)
        {
            case GameState.MITOSIS:
                {
                    canMove = false;
                    isArrived = false;
                    canPull = false;
                    break;
                }
            case GameState.PULL:
                {
                    canMove = false;
                    isArrived = false;
                    canPull = true;

                    break;
                }
            case GameState.REVERSE:
                {
                    canMove = true;
                    isArrived = false;
                    canPull = true;
                    break;
                }
            default:
                {
                    canMove = true;
                    isArrived = false;
                    canPull = true;
                    break;
                }
        }
    }
    Vector3 shootDirection = Vector3.zero;
    Rigidbody _rigidbody;

    public float shootingSpeed;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 mousePositionInWolrd = Inputs.ScreenToWorldPosition(Camera.main);
        Debug.DrawLine(transform.position, mousePositionInWolrd);

        if (listenToMouseClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 _shootDirection = mousePositionInWolrd;
                _shootDirection.z = transform.position.z;
                StartShootingWithMouse(_shootDirection);
            }

            if (Input.GetMouseButtonUp(0))
            {
                DoNotShoot();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Retraction();
            }

            if (Input.GetMouseButtonUp(1))
            {
                DoNotRetract();
            }
        }
    }

    void ClampedVelocity(Vector3 velocity, float minX, float maxX)
    {
        Vector3 velocityClamped = velocity;
        velocityClamped = new Vector3(Mathf.Clamp(velocityClamped.x, minX, maxX), velocityClamped.y, velocityClamped.z);
        _rigidbody.velocity = velocityClamped;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            isArrived = true;
            shootDirection = Vector3.zero;
            canPull = false;
            _rigidbody.useGravity = true;
            listenToMouseClick = true;
            //_rigidbody.isKinematic = true;
        }
    }
    private void FixedUpdate()
    {
        if (canPull && shootDirection != Vector3.zero && !isArrived)
        {
            Pull(shootDirection);
        }

        if (canMove)
            ClampedVelocity(_rigidbody.velocity, 0, clampMax);

    }

    private void DoNotRetract()
    {
    }

    private void Retraction()
    {
        if (!_rigidbody.isKinematic)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }
    }

    private void DoNotShoot()
    {
        shootDirection = Vector3.zero;
        if (isArrived)
        {
            _rigidbody.useGravity = true;
        }
    }

    private void StartShootingWithMouse(Vector3 _shootDirection)
    {
        canPull = true;
        isArrived = false;
        _rigidbody.isKinematic = false;
        shootDirection = _shootDirection;


    }
    private void Pull(Vector3 _shootDirection)
    {
        listenToMouseClick = false;
        _rigidbody.useGravity = false;
        Vector3 direction = _shootDirection - transform.position;
        direction.z = 0;
        _rigidbody.AddForce(direction * shootingSpeed, ForceMode.Force);
    }
}
