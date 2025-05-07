using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public TreasureThief parent; //�� Inspector ����������ű�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Debug.Log("�Ӵ�������⵽������");

            if (parent != null)
            {
                parent.OnWeaponHit();
            }
        }
    }
}
