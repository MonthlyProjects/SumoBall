using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float maxSpeed;
    [SerializeField] private SO_InputVector directionInput;
    [SerializeField] private Rigidbody rb;

    private Vector3 _moveDirection;
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }
    private void OnEnable()
    {
        directionInput.OnValueChanged += MoveInput;
    }
    private void OnDisable()
    {
        directionInput.OnValueChanged -= MoveInput;
    }
    private void FixedUpdate()
    {
        rb.AddForce(_moveDirection * force * Time.fixedDeltaTime);

        if (rb.linearVelocity.magnitude >= maxSpeed)
        {
            rb.linearVelocity = maxSpeed * rb.linearVelocity.normalized;
        }
    }
    void MoveInput(Vector2 direction)
    {
        //Projects the camera forward on 2D horizontal plane
        Vector3 camForwardOnPlane = new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z).normalized;
        Vector3 camRightOnPlane = new Vector3(_camera.transform.right.x, 0, _camera.transform.right.z).normalized;
        _moveDirection = direction.x * camRightOnPlane + direction.y * camForwardOnPlane;
    }
}
