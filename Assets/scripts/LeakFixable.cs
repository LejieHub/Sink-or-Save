using UnityEngine;

public class LeakFixable : MonoBehaviour
{
    [Header("修复设置")]
    public float fixTimeRequired = 2f;

    [Header("音效设置")]
    public AudioSource audioSource;
    public AudioClip fixingClip;

    private bool isFixing = false;
    private bool hasPlayedAudio = false;
    private float fixTimer = 0f;
    private GameObject fixingWoodPlank = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WoodPlank"))
        {
            isFixing = true;
            hasPlayedAudio = false; // 重新允许播放音效
            fixTimer = 0f;
            fixingWoodPlank = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WoodPlank") && other.gameObject == fixingWoodPlank)
        {
            isFixing = false;
            fixTimer = 0f;
            fixingWoodPlank = null;
        }
    }

    private void Update()
    {
        if (isFixing)
        {
            fixTimer += Time.deltaTime;

            // 播放音效（只播放一次）
            if (!hasPlayedAudio && audioSource != null && fixingClip != null)
            {
                audioSource.clip = fixingClip;
                audioSource.Play();
                hasPlayedAudio = true;
            }

            if (fixTimer >= fixTimeRequired)
            {
                FixLeak();
            }
        }
    }

    void FixLeak()
    {
        FindObjectOfType<WaterLevelController>()?.FixLeak();

        if (fixingWoodPlank != null)
        {
            Destroy(fixingWoodPlank);
        }

        Destroy(gameObject);
        Debug.Log("修复完成，木板消耗！");
    }
}
