using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;
    Collider _collider;
    Vector3 shootDirection = Vector3.zero;
    bool isShooting = false;
    bool isArrived = false;
    float accelation = 40;
    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWolrd = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
        //Vector3 direction = mousePositionInWolrd - transform.position;
        Debug.DrawLine(transform.position, mousePositionInWolrd);


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 _shootDirection = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
            _shootDirection.z = 0;
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
            isShooting = false;
            _rigidbody.useGravity = true;
            //_rigidbody.isKinematic = true;
        }
    }
    private void FixedUpdate()
    {
        if (isShooting && shootDirection != Vector3.zero && !isArrived)
        {
            Shoot(shootDirection);
        }
        if (isArrived)
        {
            ClampedVelocity(_rigidbody.velocity, 0, 1);
        }
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
        isShooting = true;
        isArrived = false;
        _rigidbody.isKinematic = false;
        shootDirection = _shootDirection;


    }
    private void Shoot(Vector3 _shootDirection)
    {
        _rigidbody.useGravity = false;
        Vector3 direction = _shootDirection - transform.position;
        direction.z = 0;
        _rigidbody.AddForce(direction * accelation, ForceMode.Force);
    }
}
