using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    public Material[] skyboxes;  // 存储多个 Skybox 材质
    private int currentSkyboxIndex = 0; // 记录当前 Skybox 索引

    void Start()
    {
        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[currentSkyboxIndex]; // 设置默认 Skybox
        }
    }

    public void ChangeSkyboxMaterial()
    {
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Length; // 切换到下一个 Skybox
        RenderSettings.skybox = skyboxes[currentSkyboxIndex]; // 应用新的 Skybox
        DynamicGI.UpdateEnvironment(); // 更新全局光照
    }
}
