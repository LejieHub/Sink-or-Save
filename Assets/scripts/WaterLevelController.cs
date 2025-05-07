using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterLevelController : MonoBehaviour
{
    public Transform waterSurface;    // ��ĺ������
    public float riseSpeedPerHole = 0.02f;  // ÿ��©��ÿ��ˮ���ǵ��ٶ�
    public float maxWaterHeight = 5f;  // ��������߶���Ϸʧ��

    public float gameDuration = 300f;  // ��Ϸ��ʱ��������5���ӣ�
    private float gameTimer;

    private int activeLeaks = 0;       // ��ǰ���ڶ��ٸ�©��
    private bool gameEnded = false;

    [Header("��������")]
    public TreasureChest chest1;
    public TreasureChest chest2;

    void Start()
    {
        gameTimer = gameDuration;
    }

    void Update()
    {
        if (gameEnded) return;

        // 1. ˮλ�����߼�
        if (activeLeaks > 0)
        {
            float riseAmount = riseSpeedPerHole * activeLeaks * Time.deltaTime;
            waterSurface.position += new Vector3(0, riseAmount, 0);
        }

        // 2. ���ˮλ�Ƿ񵽴����
        if (waterSurface.position.y >= maxWaterHeight)
        {
            GameFail();
        }

        // 3. ����ʱ�߼�
        gameTimer -= Time.deltaTime;
        if (gameTimer <= 0)
        {
            GameSuccess();
        }
    }

    public void AddLeak()
    {
        activeLeaks++;
    }

    public void FixLeak()
    {
        activeLeaks = Mathf.Max(0, activeLeaks - 1);
    }

    void GameFail()
    {
        Debug.Log("����û�ˣ�");
        gameEnded = true;

        SaveTreasureData();
        PlayerPrefs.SetInt("IsWin", 0); // 0��ʾʧ��
        PlayerPrefs.Save();
        SceneManager.LoadScene("EndingScene"); // �е�����Scene
    }

    void GameSuccess()
    {
        Debug.Log("�ɹ���ֵ����");
        gameEnded = true;

        SaveTreasureData();
        PlayerPrefs.SetInt("IsWin", 1); // 1��ʾʤ��
        PlayerPrefs.Save();
        SceneManager.LoadScene("EndingScene"); // �е�����Scene
    }

    void SaveTreasureData()
    {
        if (chest1 != null)
            PlayerPrefs.SetInt("Chest1Value", chest1.currentValue);

        if (chest2 != null)
            PlayerPrefs.SetInt("Chest2Value", chest2.currentValue);

        PlayerPrefs.Save();
    }
}
