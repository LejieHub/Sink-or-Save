using UnityEngine;
using System.Collections.Generic;

public class TreasureThiefSpawner : MonoBehaviour
{
    public List<GameObject> monsterPrefabs;
    public List<Transform> spawnPoints;

    public List<Transform> possibleTreasureTargets;       // ����λ��
    public List<TreasureChest> possibleTreasureChests;    // �����߼��ű�
    public List<Transform> possibleExitTargets;           // ������

    public float spawnInterval = 5f;
    public int maxActiveMonsters = 10;

    private List<GameObject> activeMonsters = new();

    void Start()
    {
        InvokeRepeating(nameof(SpawnMonster), 1f, spawnInterval);
    }

    void SpawnMonster()
    {
        // ���ʧЧ��������
        activeMonsters.RemoveAll(m => m == null);

        if (activeMonsters.Count >= maxActiveMonsters) return;
        if (monsterPrefabs.Count == 0 || spawnPoints.Count == 0) return;

        // �����ѡ
        GameObject prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        int index = Random.Range(0, possibleTreasureChests.Count);
        TreasureChest randomChest = possibleTreasureChests[index];
        Transform randomTarget = randomChest.transform; // �� chest ����ͬ��

        Transform randomExit = possibleExitTargets[Random.Range(0, possibleExitTargets.Count)];

        // ʵ����
        GameObject monster = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        activeMonsters.Add(monster);

        // ע������
        TreasureThief thief = monster.GetComponent<TreasureThief>();
        if (thief != null)
        {
            thief.target = randomTarget;
            thief.targetChest = randomChest;
            thief.exitTarget = randomExit;
        }
    }
}
