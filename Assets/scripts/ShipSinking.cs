using UnityEngine;

public class ShipSinking : MonoBehaviour
{
    public float sinkSpeed = 0.5f; // �³��ٶ�
    public float sinkDuration = 5f; // �³�ʱ��
    public GameObject uiToShowAfterSink; // �³���ɺ�Ҫ��ʾ��UI

    private float timer = 0f;
    private bool sinking = true;

    void Update()
    {
        if (!sinking) return;

        transform.position += Vector3.down * sinkSpeed * Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= sinkDuration)
        {
            sinking = false;

            if (uiToShowAfterSink != null)
            {
                uiToShowAfterSink.SetActive(true);
            }
        }
    }
}
