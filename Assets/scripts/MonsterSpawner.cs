using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("刷新设置")]
    public float spawnInterval = 5f;
    public int maxActiveMonsters = 10;

    [Header("怪物预设（可拖多个）")]
    public List<GameObject> monsterPrefabs;

    [Header("生成点（可拖多个）")]
    public List<Transform> spawnPoints;

    private List<GameObject> activeMonsters = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // 移除已被销毁的怪物引用
            activeMonsters.RemoveAll(monster => monster == null);

            if (activeMonsters.Count >= maxActiveMonsters) continue;

            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        if (monsterPrefabs.Count == 0 || spawnPoints.Count == 0) return;

        // 随机选一个怪物和位置
        GameObject prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // 实例化怪物
        GameObject newMonster = Instantiate(prefab, point.position, point.rotation);
        activeMonsters.Add(newMonster);
    }
}
