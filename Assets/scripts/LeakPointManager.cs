using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeakPointManager : MonoBehaviour
{
    [Header("漏水点位")]
    public List<Transform> leakPoints;

    [Header("漏水效果Prefab")]
    public GameObject leakEffectPrefab;

    [Header("生成控制")]
    public float leakInterval = 10f;

    private WaterLevelController waterLevelController; // 引用水位控制器

    private void Start()
    {
        waterLevelController = FindObjectOfType<WaterLevelController>(); // 找到水位管理器

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

        // 每生成一个新破洞，就通知 WaterLevelController
        if (waterLevelController != null)
        {
            waterLevelController.AddLeak();
        }
    }
}
