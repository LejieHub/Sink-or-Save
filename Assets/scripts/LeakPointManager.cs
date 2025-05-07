using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeakPointManager : MonoBehaviour
{
    [Header("©ˮ��λ")]
    public List<Transform> leakPoints;

    [Header("©ˮЧ��Prefab")]
    public GameObject leakEffectPrefab;

    [Header("���ɿ���")]
    public float leakInterval = 10f;

    private WaterLevelController waterLevelController; // ����ˮλ������

    private void Start()
    {
        waterLevelController = FindObjectOfType<WaterLevelController>(); // �ҵ�ˮλ������

        StartCoroutine(SpawnLeakLoop());
    }

    IEnumerator SpawnLeakLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(leakInterval);

            SpawnLeak();
        }
    }

    void SpawnLeak()
    {
        if (leakPoints.Count == 0) return;

        Transform randomPoint = leakPoints[Random.Range(0, leakPoints.Count)];
        Instantiate(leakEffectPrefab, randomPoint.position, randomPoint.rotation, randomPoint);

        // ÿ����һ�����ƶ�����֪ͨ WaterLevelController
        if (waterLevelController != null)
        {
            waterLevelController.AddLeak();
        }
    }
}
