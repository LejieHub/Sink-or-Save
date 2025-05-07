using UnityEngine;

public class TreasureTrigger : MonoBehaviour
{
    public TreasureChest linkedChest; // 关联的 TreasureChest 脚本
    public AudioSource audioSource;   // 播放器组件
    public AudioClip collectClip;     // 要播放的声音片段

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<TreasureItem>();
        if (item != null && !item.IsCollected && linkedChest != null)
        {
            linkedChest.AddTreasure(item.value);

            // 播放音效（确保有 audioSource 和 clip）
            if (audioSource != null && collectClip != null)
            {
                audioSource.PlayOneShot(collectClip);
            }

            Destroy(other.gameObject);
        }
    }
}
