using UnityEngine;
using System.Collections.Generic;

public class SkullCollection : MonoBehaviour
{
    [Header("Skull Collection")]
    public List<GameObject> skulls = new List<GameObject>(); // **在 Unity 里拖拽骷髅进去**
    private int collectedSkulls = 0; // **记录已收集的骷髅数量**

    [Header("Reward Settings")]
    public GameObject rewardObject; // **可以是宝箱、提示 UI 或其他奖励**
    public bool unlockReward = false; // **是否解锁奖励**

    public AudioSource unlockSound;

    void Start()
    {
        if (skulls.Count == 0)
        {
            Debug.LogWarning("No skulls assigned! Please assign skull objects in the Inspector.");
        }
    }

    public void CollectSkull(GameObject skull)
    {
        skulls.Remove(skull); // **从 List 中移除这个骷髅**
        Destroy(skull); // **销毁骷髅对象**
        collectedSkulls++; // **增加收集计数**
        Debug.Log("Skull collected! " + collectedSkulls + "/6");

        if (collectedSkulls >= 6) // **如果收集满 6 个**
        {
            UnlockReward();
        }
    }

    void UnlockReward()
    {
        unlockReward = true; // **标记奖励解锁**
        Debug.Log("All skulls collected! Reward unlocked!");

        if (rewardObject != null)
        {
            rewardObject.SetActive(true); // **显示奖励（比如打开宝箱、显示 UI）**
            unlockSound.Play();
        }
    }
}
