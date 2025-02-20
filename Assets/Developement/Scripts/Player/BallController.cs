using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SO_InputVector sO_InputVector;
    [SerializeField] private float minTimeBetweenPush;
    [SerializeField] private float pushBetweenPlayersVelocityMultiplier;
    public float PushForce { get { return pushBetweenPlayersVelocityMultiplier; } set {  pushBetweenPlayersVelocityMultiplier = value; } }

    private Coroutine fixedUpdate;

    private Vector3 _moveDirection;
    private Camera _camera;
    private float _timeSinceLastPush;
    private void Awake()
    {
        _camera = Camera.main;
        _timeSinceLastPush = Time.time;
    }

    public void Initialize(SO_InputVector sO_InputVector)
    {
        this.sO_InputVector = sO_InputVector;
        Initialize();
    }

    [EasyButtons.Button]
    public void Initialize () 
    {
        sO_InputVector.OnValueChanged += MoveInput;
    }

    public void DeInitialize ()
    {
        sO_InputVector.OnValueChanged -= MoveInput;
    }

    [EasyButtons.Button]
    public void Lauch (bool lauch)
    {
        if(lauch && fixedUpdate == null)
        {
            fixedUpdate = StartCoroutine(CorouFixedUpdate());
            return;
        }
        else if (!lauch && fixedUpdate != null) 
        {
            StopCoroutine(fixedUpdate);
            fixedUpdate = null;
        }
    }

    IEnumerator CorouFixedUpdate ()
    {
        yield return null;

        while (true)
        {
            Debug.Log("Je Suis en update");
            Move();
            yield return new WaitForFixedUpdate();
        }
    }


    public void MoveInput(Vector2 direction)
    {
        //Projects the camera forward on 2D horizontal plane
        Vector3 camForwardOnPlane = new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z).normalized;
        Vector3 camRightOnPlane = new Vector3(_camera.transform.right.x, 0, _camera.transform.right.z).normalized;
        _moveDirection = direction.x * camRightOnPlane + direction.y * camForwardOnPlane;
    }

    public void Move ()
    {
        rb.AddForce(_moveDirection * force * Time.fixedDeltaTime);

        if (rb.linearVelocity.magnitude >= maxSpeed)
        {
            rb.linearVelocity = maxSpeed * rb.linearVelocity.normalized;
        }
    }

    void PushBothPlayer(BallController ballController2)
    {
        // Calculate the normal vector
        Vector3 collisionNormal = (rb.position - ballController2.transform.position).normalized;

        // Calculate relative velocity
        Vector3 relativeVelocity = GetVelocity() - ballController2.GetVelocity();

        // Calculate the velocity components along the normal
        float velocityAlongNormal = Vector3.Dot(relativeVelocity, collisionNormal);

        // If velocities are separating, do nothing
        if (velocityAlongNormal > 0) return;

        // Apply boost factor to the velocity along the normal
        float boostedVelocityBall1 = velocityAlongNormal * ballController2.PushForce;
        float boostedVelocityBall2 = velocityAlongNormal * pushBetweenPlayersVelocityMultiplier;


        // Calculate new velocities (for equal masses in a perfectly elastic collision)
        Vector3 velocityBall1 = (GetVelocity()                 - boostedVelocityBall1 * collisionNormal) * Mathf.Clamp(ballController2.GetVelocity().magnitude / GetVelocity().magnitude,0.5f,2f);
        Vector3 velocityBall2 = (ballController2.GetVelocity() + boostedVelocityBall2 * collisionNormal) * Mathf.Clamp(GetVelocity().magnitude / ballController2.GetVelocity().magnitude,0.5f,2f);

        SetPushVelocity(velocityBall1);
        ballController2.SetPushVelocity(velocityBall2);
    }
    public void SetPushTime()
    {
        _timeSinceLastPush = Time.time;
    }
    public void SetPushVelocity(Vector3 velocity)
    {
        if(CanBePushed())
        {
            rb.linearVelocity = velocity;
            SetPushTime();
        }
    }
    public Vector3 GetVelocity()
    {
        return rb.linearVelocity;
    }
    private bool CanBePushed()
    {
        return (Time.time - _timeSinceLastPush) > minTimeBetweenPush;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<BallController>(out BallController ballController))
        {
            PushBothPlayer(ballController);
        }
    }

    private void OnDisable()
    {
        Lauch(false);
    }

    [Header("GIZMOS")]
    [SerializeField] Vector3 ball1VelocityDir;
    [SerializeField] Vector3 ball2VelocityDir;
    [SerializeField] float ball1velMagnitude;
    [SerializeField] float ball2velMagnitude;

    [SerializeField] Vector3 ball1Position;
    [SerializeField] Vector3 ball2Position;

    [SerializeField] Color velocityColor;
    [SerializeField] Color collisionResultVelocityColorball1;
    [SerializeField] Color collisionResultVelocityColorball2;

    [SerializeField] Color ball1Color;
    [SerializeField] Color ball2Color;
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = ball1Color;
        Gizmos.DrawSphere(ball1Position, 0.5f);

        Gizmos.color = ball2Color;
        Gizmos.DrawSphere(ball2Position, 0.5f);

        Gizmos.color = velocityColor;
        Vector3 vel1 = ball1VelocityDir.normalized * ball1velMagnitude;
        Vector3 vel2 = ball2VelocityDir.normalized * ball2velMagnitude;
        Gizmos.DrawLine(ball1Position, ball1Position+vel1);
        Gizmos.DrawLine(ball2Position, ball2Position+vel2);

        // Calculate the normal vector
        Vector3 collisionNormal = (ball1Position - ball2Position).normalized;

        // Calculate relative velocity
        Vector3 relativeVelocity = vel1 - vel2;

        // Calculate the velocity components along the normal
        float velocityAlongNormal = Vector3.Dot(relativeVelocity, collisionNormal);

        // If velocities are separating, do nothing
        if (velocityAlongNormal > 0) return;

        // Apply boost factor to the velocity along the normal
        float boostedVelocity = velocityAlongNormal * pushBetweenPlayersVelocityMultiplier;

        // Calculate new velocities (for equal masses in a perfectly elastic collision)
        Vector3 velocityBall1 = (vel1 - boostedVelocity * collisionNormal) * vel2.magnitude / vel1.magnitude;
        Vector3 velocityBall2 = (vel2 + boostedVelocity * collisionNormal) * vel1.magnitude / vel2.magnitude;

        Gizmos.color = collisionResultVelocityColorball1;
        Gizmos.DrawLine(ball1Position, ball1Position + velocityBall1);

        Gizmos.color = collisionResultVelocityColorball2;
        Gizmos.DrawLine(ball2Position, ball2Position + velocityBall2);
    }
}
