using UnityEngine;
using UnityEngine.SceneManagement;

public class OceanCollision : MonoBehaviour
{
    [SerializeField] private GameObject player; // 手动拖拽玩家对象
    [SerializeField] private string sceneToLoad; // 手动设置场景名称

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) // 确保是玩家
        {
            SceneManager.LoadScene(sceneToLoad); // 切换场景
        }
    }
}
