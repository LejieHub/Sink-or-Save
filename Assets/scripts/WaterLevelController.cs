using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterLevelController : MonoBehaviour
{
    public Transform waterSurface;    // 你的海面对象
    public float riseSpeedPerHole = 0.02f;  // 每个漏洞每秒水上涨的速度
    public float maxWaterHeight = 5f;  // 到达这个高度游戏失败

    public float gameDuration = 300f;  // 游戏总时长（比如5分钟）
    private float gameTimer;

    private int activeLeaks = 0;       // 当前存在多少个漏洞
    private bool gameEnded = false;

    [Header("宝箱引用")]
    public TreasureChest chest1;
    public TreasureChest chest2;

    void Start()
    {
        gameTimer = gameDuration;
    }

    void Update()
    {
        if (gameEnded) return;

        // 1. 水位上涨逻辑
        if (activeLeaks > 0)
        {
            float riseAmount = riseSpeedPerHole * activeLeaks * Time.deltaTime;
            waterSurface.position += new Vector3(0, riseAmount, 0);
        }

        // 2. 检查水位是否到达最大
        if (waterSurface.position.y >= maxWaterHeight)
        {
            GameFail();
        }

        // 3. 倒计时逻辑
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
        Debug.Log("船沉没了！");
        gameEnded = true;

        SaveTreasureData();
        PlayerPrefs.SetInt("IsWin", 0); // 0表示失败
        PlayerPrefs.Save();
        SceneManager.LoadScene("EndingScene"); // 切到结算Scene
    }

    void GameSuccess()
    {
        Debug.Log("成功坚持到最后！");
        gameEnded = true;

        SaveTreasureData();
        PlayerPrefs.SetInt("IsWin", 1); // 1表示胜利
        PlayerPrefs.Save();
        SceneManager.LoadScene("EndingScene"); // 切到结算Scene
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
