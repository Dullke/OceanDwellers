using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] [BoxGroup("Required Data")] private SOGameData gameData;
    [SerializeField] [BoxGroup("Required Data")] private GameObject healthBar;
    [Tooltip("O componente 'Transform' do objeto 'Canvas' onde as barras de vida serão instanciadas.")]
    [SerializeField] [BoxGroup("Required Data")] private Transform healthCanvas;

    [SerializeField] [BoxGroup("Spawner Properties")] [Tooltip("Needs to be sorted from lowest to highest weight")]
    private List<EnemySpawnWeight> enemyWeights = new List<EnemySpawnWeight>();

    private Camera mainCamera;
    private int timeBetweenSpawns;

    private void Awake()
    {
        timeBetweenSpawns = (int)gameData.TimeBetweenEnemySpawns;
        mainCamera = FindObjectOfType<Camera>();
        StartSpawner();
    }
    public void StartSpawner()
    {
        StartCoroutine(BeginSpawning());
    }

    private void SpawnEnemy(GameObject enemy)
    {
        GameObject chaserObject = Instantiate(enemy, GetPositionOutsideCamera(), Quaternion.identity);
        EnemyData data = chaserObject.GetComponent<EnemyData>();

        GameObject healthBarObject = Instantiate(healthBar, chaserObject.transform.position, Quaternion.identity, healthCanvas);
        healthBarObject.GetComponent<HealthBar>().shipData = data;
    }
    private Vector2 GetPositionOutsideCamera()
    {
        float cameraSize = mainCamera.orthographicSize;
        Vector2 randomPosition = new Vector2(Random.Range(-30, 30),
                                             Random.Range(-30, 30));

        if (randomPosition.x < -cameraSize * 2 || randomPosition.x > cameraSize * 2 ||
            randomPosition.y < -cameraSize * 2 || randomPosition.y > cameraSize * 2)
        {
            return randomPosition;
        }

        return GetPositionOutsideCamera();
    }

    private IEnumerator BeginSpawning()
    {
        while (true)
        {
            GameObject enemyToSpawn = GetRandomWeightedEnemy();
            SpawnEnemy(enemyToSpawn);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
    private GameObject GetRandomWeightedEnemy()
    {
        int random = Random.Range(0, 101);
        foreach (EnemySpawnWeight weightedEnemy in enemyWeights)
            if (random <= weightedEnemy.weight) return weightedEnemy.enemy;

        return enemyWeights[enemyWeights.Count - 1].enemy;
    }


    [System.Serializable]
    internal struct EnemySpawnWeight
    {
        EnemySpawnWeight(GameObject newEnemy, int newWeight)
        {
            enemy = newEnemy;
            weight = newWeight;
        }

        public GameObject enemy;
        [Range(0, 100)]
        public int weight;
    }
}
