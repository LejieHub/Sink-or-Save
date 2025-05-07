using UnityEngine;
using System.Collections.Generic;

public class TreasureThiefSpawner : MonoBehaviour
{
    public List<GameObject> monsterPrefabs;
    public List<Transform> spawnPoints;

    public List<Transform> possibleTreasureTargets;       // 宝箱位置
    public List<TreasureChest> possibleTreasureChests;    // 宝箱逻辑脚本
    public List<Transform> possibleExitTargets;           // 跳海点

    public float spawnInterval = 5f;
    public int maxActiveMonsters = 10;

    private List<GameObject> activeMonsters = new();

    void Start()
    {
        InvokeRepeating(nameof(SpawnMonster), 1f, spawnInterval);
    }

    void SpawnMonster()
    {
        // 清除失效怪物引用
        activeMonsters.RemoveAll(m => m == null);

        if (activeMonsters.Count >= maxActiveMonsters) return;
        if (monsterPrefabs.Count == 0 || spawnPoints.Count == 0) return;

        // 随机挑选
        GameObject prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        int index = Random.Range(0, possibleTreasureChests.Count);
        TreasureChest randomChest = possibleTreasureChests[index];
        Transform randomTarget = randomChest.transform; // 和 chest 保持同步

        Transform randomExit = possibleExitTargets[Random.Range(0, possibleExitTargets.Count)];

        // 实例化
        GameObject monster = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        activeMonsters.Add(monster);

        // 注入数据
        TreasureThief thief = monster.GetComponent<TreasureThief>();
        if (thief != null)
        {
            thief.target = randomTarget;
            thief.targetChest = randomChest;
            thief.exitTarget = randomExit;
        }
    }
}
