using UnityEngine;

public class LeakFixable : MonoBehaviour
{
    [Header("�޸�����")]
    public float fixTimeRequired = 2f;

    [Header("��Ч����")]
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
            hasPlayedAudio = false; // ������������Ч
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

            // ������Ч��ֻ����һ�Σ�
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
        Debug.Log("�޸���ɣ�ľ�����ģ�");
    }
}
