using UnityEngine;

public class ShipSinking : MonoBehaviour
{
    public float sinkSpeed = 0.5f; // 下沉速度
    public float sinkDuration = 5f; // 下沉时间
    public GameObject uiToShowAfterSink; // 下沉完成后要显示的UI

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
