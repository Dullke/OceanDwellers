using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Cannons))]
public class EnemyCannons : MonoBehaviour
{
    [SerializeField] [BoxGroup("Required Data")] private Cannons cannonsModule;

    [SerializeField] [BoxGroup("Enemy Properties")] private Transform[] detections;
    [SerializeField] [BoxGroup("Enemy Properties")] private LayerMask playerMask;
    [SerializeField] [BoxGroup("Enemy Properties")] private int maxDetectionDistance;

    private void Awake()
    {
        StartCoroutine(DetectPlayer());
    }

    private IEnumerator DetectPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < detections.Length; i++)
            {
                if (Physics2D.Raycast(detections[i].position, detections[i].up, maxDetectionDistance, playerMask))
                {
                    cannonsModule.UseCannons(i);
                }
            }
        }
    }

}
