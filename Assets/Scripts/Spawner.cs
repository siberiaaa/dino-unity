
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;

        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;

    public float minSpawnTime = 1f;
    public float maxSpawnTime = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), UnityEngine.Random.Range(minSpawnTime, maxSpawnTime));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = UnityEngine.Random.value;

        foreach (var obj in objects)
        {
            if(spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break;
            }
            else
            {
                spawnChance -= obj.spawnChance;
            }
        }

        Invoke(nameof(Spawn), UnityEngine.Random.Range(minSpawnTime, maxSpawnTime));
    }

}
