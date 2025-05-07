using UnityEngine;

public class TreasureItem : MonoBehaviour
{
    public enum TreasureType { Coin, Gem, GoldBar, Trophy }
    public TreasureType type;

    [Range(1, 100)]
    public int value; // 宝藏价值

    [Tooltip("怪物偷取的几率权重，越高越容易被偷")]
    public float weight = 1.0f;

    public bool IsCollected => value <= 0;
}
