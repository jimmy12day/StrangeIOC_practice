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
    float accelation = 10;
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 _shootDirection = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
            _shootDirection.z = 0;
            StartShootingWithMouse(_shootDirection);
        }
        else
        if (Input.GetMouseButtonUp(0))
        {
            DoNotShoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Retraction();
        }
        else
        if (Input.GetMouseButtonUp(1))
        {
            DoNotRetract();
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            isArrived = true;
            shootDirection = Vector3.zero;
            _rigidbody.isKinematic = true;
        }
    }
    private void FixedUpdate()
    {
        if (isShooting && shootDirection != Vector3.zero && !isArrived)
        {
            Shoot(shootDirection);
        }
    }

    private void DoNotRetract()
    {
    }

    private void Retraction()
    {
    }

    private void DoNotShoot()
    {
        shootDirection = Vector3.zero;
    }

    private void StartShootingWithMouse(Vector3 _shootDirection)
    {
        isShooting = true;
        _rigidbody.isKinematic = false;
        shootDirection = _shootDirection;


    }
    private void Shoot(Vector3 _shootDirection)
    {
        Debug.Log("mousePosition|" + _shootDirection);
        Vector3 direction = _shootDirection - transform.position;
        direction.z = 0;
        Debug.Log("direction|" + direction);
        Debug.Log("position|" + transform.position);
        _rigidbody.AddForce(direction * accelation, ForceMode.Force);
    }
}
