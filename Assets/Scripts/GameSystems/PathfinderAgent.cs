using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class PathfinderAgent : MonoBehaviour
{
    [BoxGroup("Required Data")]
    [SerializeField] [TitleGroup("Required Data/Components")] private Rigidbody2D agent;

    [SerializeField] [BoxGroup("Agent Properties")] private int stoppingDistance;
    [SerializeField] [BoxGroup("Agent Properties")] private float timeBetweenPathUpdates = 0.6f;
    [SerializeField] [BoxGroup("Movement Values")] private int speed;
    [Tooltip("A resistência do objeto ao se mover, menores valores aumentarão a aceleração.")]
    [SerializeField] [BoxGroup("Movement Values")] private float moveDrag = 0.2f;
    [Tooltip("A resistência do objeto a rotação, menores valores aumentarão a aceleração de rotação")]
    [SerializeField] [BoxGroup("Movement Values")] private float rotationDrag = 0.2f;

    private Pathfinder navigationMap;
    private Transform player;

    private Vector2 currentWaypoint;
    private Transform currentDestination;
    private Queue<Vector2> currentPath = new Queue<Vector2>();

    private Vector2 smoothForce;
    private Vector2 refCurrentVelocity;

    private float smoothAngle;
    private float refCurrentAngularVelocity;


    private void Awake()
    {
        navigationMap = FindObjectOfType<Pathfinder>();
        player = FindObjectOfType<PlayerMovement>().transform;

        SetDestination(player);
    }

    public void SetDestination(Transform destination)
    {
        currentDestination = destination;
        currentPath = navigationMap.FindPath(agent.position, currentDestination.position);

        StartCoroutine(UpdatePath());
        StartCoroutine(MoveAgent());
        StartCoroutine(RotateAgent());
    }

    private IEnumerator UpdatePath()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenPathUpdates);
            if (currentDestination == null) break;

            currentPath = navigationMap.FindPath(agent.position, currentDestination.position);

            if (currentPath.Count == 0) continue;
            currentWaypoint = currentPath.Dequeue();
        }
    }

    private IEnumerator MoveAgent()
    {
        while (currentDestination != null)
        {
            yield return null;
            if (currentPath.Count == 0) continue;
            
            currentWaypoint = currentPath.Dequeue();
            while ((agent.position - currentWaypoint).sqrMagnitude > Mathf.Pow(stoppingDistance, 2))
            {
                smoothForce = Vector2.SmoothDamp(smoothForce, 10 * speed * agent.transform.up * Time.fixedDeltaTime, ref refCurrentVelocity, moveDrag);
                agent.velocity = smoothForce;

                yield return null;
            }
        }
    }

    private IEnumerator RotateAgent()
    {
        while (currentDestination != null)
        {
            Vector2 direction = (currentWaypoint - agent.position).normalized;
            float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            smoothAngle = Mathf.SmoothDampAngle(smoothAngle, angle, ref refCurrentAngularVelocity, rotationDrag);
            agent.transform.eulerAngles = Vector3.forward * smoothAngle;

            yield return null;
        }
    }

}
