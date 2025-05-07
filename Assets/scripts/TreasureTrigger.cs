using UnityEngine;

public class TreasureTrigger : MonoBehaviour
{
    public TreasureChest linkedChest; // ������ TreasureChest �ű�
    public AudioSource audioSource;   // ���������
    public AudioClip collectClip;     // Ҫ���ŵ�����Ƭ��

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<TreasureItem>();
        if (item != null && !item.IsCollected && linkedChest != null)
        {
            linkedChest.AddTreasure(item.value);

            // ������Ч��ȷ���� audioSource �� clip��
            if (audioSource != null && collectClip != null)
            {
                audioSource.PlayOneShot(collectClip);
            }

            Destroy(other.gameObject);
        }
    }
}
