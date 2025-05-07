using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("ˢ������")]
    public float spawnInterval = 5f;
    public int maxActiveMonsters = 10;

    [Header("����Ԥ�裨���϶����")]
    public List<GameObject> monsterPrefabs;

    [Header("���ɵ㣨���϶����")]
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

            // �Ƴ��ѱ����ٵĹ�������
            activeMonsters.RemoveAll(monster => monster == null);

            if (activeMonsters.Count >= maxActiveMonsters) continue;

            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        if (monsterPrefabs.Count == 0 || spawnPoints.Count == 0) return;

        // ���ѡһ�������λ��
        GameObject prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // ʵ��������
        GameObject newMonster = Instantiate(prefab, point.position, point.rotation);
        activeMonsters.Add(newMonster);
    }
}
