using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public TreasureThief parent; //从 Inspector 传入主怪物脚本

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Debug.Log("子触发器检测到武器！");

            if (parent != null)
            {
                parent.OnWeaponHit();
            }
        }
    }
}
