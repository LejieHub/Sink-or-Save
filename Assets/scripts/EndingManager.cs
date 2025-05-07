using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject winGroup;
    public GameObject loseGroup;

    [Header("Skybox Settings")]
    public Material winSkybox;    // ʤ���õ���պв���
    public Material loseSkybox;   // ʧ���õ���պв���

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
