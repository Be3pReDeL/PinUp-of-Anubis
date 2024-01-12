using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    [System.Serializable]
    public class PlatformPrefab {
        public GameObject prefab;
        public float spawnChance = 1f;
    }

    [System.Serializable]
    public class ItemPrefab {
        public GameObject prefab;
        public float spawnChance = 0.5f; // 50% вероятность спауна
    }

    [System.Serializable]
    public class EnemyPrefab {
        public GameObject prefab;
        public float spawnChance = 0.3f; // 30% вероятность спауна
    }

    [SerializeField] private PlatformPrefab[] _platformPrefabs;
    [SerializeField] private ItemPrefab[] _itemPrefabs; // Монеты, буквы
    [SerializeField] private EnemyPrefab[] _enemyPrefabs; // Враги
    [SerializeField] private GameObject _characterPrefab; // Префаб персонажа

    [SerializeField] private int _numberOfPlatforms = 10;
    [SerializeField] private float _levelWidth = 3f;
    [SerializeField] private float _minY = .2f;
    [SerializeField] private float _maxY = 1.5f;
    [SerializeField] private float _itemHeightAbovePlatform = 0.5f; // Высота над платформой

    private Transform lastSpawnedPlatform;

    void Start() {
        Vector3 spawnPosition = new Vector3();
        
        for (int i = 0; i < _numberOfPlatforms; i++) {
            spawnPosition.y += Random.Range(_minY, _maxY);
            spawnPosition.x = Random.Range(-_levelWidth, _levelWidth);

            PlatformPrefab chosenPlatform = ChooseRandom(_platformPrefabs);
            GameObject platform = Instantiate(chosenPlatform.prefab, spawnPosition, Quaternion.identity, transform);

            lastSpawnedPlatform = platform.transform;

            SpawnItemsOnPlatform(platform.transform);
            SpawnEnemiesOnPlatform(platform.transform);
        }

        // Спауним персонажа
        SpawnCharacter();
    }

    private T ChooseRandom<T>(T[] prefabs) where T : class {
        float totalChance = 0f;
        foreach (var prefab in prefabs) {
            dynamic p = prefab;
            totalChance += p.spawnChance;
        }

        float randomPoint = Random.value * totalChance;
        for (int i = 0; i < prefabs.Length; i++) {
            dynamic p = prefabs[i];
            if (randomPoint < p.spawnChance)
                return p;
            randomPoint -= p.spawnChance;
        }
        return prefabs[prefabs.Length - 1];
    }

    private void SpawnItemsOnPlatform(Transform platformTransform) {
        foreach (var itemPrefab in _itemPrefabs) {
            if (Random.value < itemPrefab.spawnChance) {
                Vector3 spawnPos = platformTransform.position + new Vector3(0, _itemHeightAbovePlatform, 0);
                spawnPos.x += Random.Range(-platformTransform.localScale.x / 2, platformTransform.localScale.x / 2);
                Instantiate(itemPrefab.prefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }

    private void SpawnEnemiesOnPlatform(Transform platformTransform) {
        foreach (var enemyPrefab in _enemyPrefabs) {
            if (Random.value < enemyPrefab.spawnChance) {
                Vector3 spawnPos = platformTransform.position + new Vector3(0, _itemHeightAbovePlatform, 0);
                spawnPos.x += Random.Range(-platformTransform.localScale.x / 2, platformTransform.localScale.x / 2);
                Instantiate(enemyPrefab.prefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }

    private void SpawnCharacter() {
        if (lastSpawnedPlatform != null) {
            Vector3 spawnPos = lastSpawnedPlatform.position;
            spawnPos.y += 1f; // Смещение по оси Y для спауна персонажа над платформой
            Instantiate(_characterPrefab, spawnPos + new Vector3(0f, _itemHeightAbovePlatform, 0f), Quaternion.identity, transform);
        }
    }
}
