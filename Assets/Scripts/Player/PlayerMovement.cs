using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [BoxGroup("Required Data")] private SOInputReader inputReader;
    [SerializeField] [BoxGroup("Movement Values")] private int speed = 4;

    [Tooltip("A resistência do objeto ao se mover, menores valores aumentarão a aceleração.")]
    [SerializeField] [BoxGroup("Movement Values")] private float moveDrag = 0.2f;
    [SerializeField] [BoxGroup("Movement Values")] private int rotationSpeed = 4;

    [Tooltip("A resistência do objeto a rotação, menores valores aumentarão a aceleração de rotação")]
    [SerializeField] [BoxGroup("Movement Values")] private float rotationDrag = 0.2f;

    private Rigidbody2D rigidBody;
    private Camera mainCamera;

    private Vector2 moveAxis;
    private Vector2 smoothForce;
    private Vector2 refCurrentVelocity;

    private float smoothAngle;
    private float refCurrentAngularVelocity;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = FindObjectOfType<Camera>();
    }

    public void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();

        float cameraSize = mainCamera.orthographicSize;
        if (transform.position.x < -cameraSize - 8 ||transform.position.y < -cameraSize)
        {
            transform.position = transform.position - DirectionToOrigin();
        }

        if (transform.position.x > cameraSize + 8 || transform.position.y > cameraSize)
        {
            transform.position = transform.position + DirectionToOrigin();
        }
    }

    private Vector3 DirectionToOrigin()
    {
        Vector3 absoluteTransform = new Vector3(Mathf.Abs(transform.position.x), Mathf.Abs(transform.position.y));
        return (Vector3.zero - absoluteTransform).normalized;
    }

    private void HandleMovement()
    {
        Vector2 force = (transform.up * moveAxis.y * speed * 10) *  Time.fixedDeltaTime;
        smoothForce = Vector2.SmoothDamp(smoothForce, force, ref refCurrentVelocity, moveDrag);

        rigidBody.velocity = smoothForce;
    }

    private void HandleRotation()
    {
        int angularVelocityMultiplier = 360;
        if (rigidBody.velocity.magnitude < 0.2f) angularVelocityMultiplier = 0;

        float angleTarget = (-moveAxis.x * rotationSpeed * angularVelocityMultiplier) * Time.fixedDeltaTime;
        smoothAngle = Mathf.SmoothDampAngle(smoothAngle, angleTarget, ref refCurrentAngularVelocity, rotationDrag);

        rigidBody.angularVelocity = smoothAngle;
    }

    #region Event Listeners

    private void GetMoveAxis(Vector2 inputAxis) => moveAxis = inputAxis;

    private void OnEnable()
    {
        inputReader.onMove += GetMoveAxis;
    }
    private void OnDisable()
    {
        inputReader.onMove -= GetMoveAxis;
    }

    #endregion
}
