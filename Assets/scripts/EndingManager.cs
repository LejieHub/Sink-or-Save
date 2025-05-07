using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject winGroup;
    public GameObject loseGroup;

    [Header("Skybox Settings")]
    public Material winSkybox;    // 胜利用的天空盒材质
    public Material loseSkybox;   // 失败用的天空盒材质

    void Start()
    {
        int isWin = PlayerPrefs.GetInt("IsWin", 0);

        if (isWin == 1)
        {
            winGroup.SetActive(true);
            loseGroup.SetActive(false);

            if (winSkybox != null)
                RenderSettings.skybox = winSkybox;
        }
        else
        {
            winGroup.SetActive(false);
            loseGroup.SetActive(true);

            if (loseSkybox != null)
                RenderSettings.skybox = loseSkybox;
        }
    }
}
