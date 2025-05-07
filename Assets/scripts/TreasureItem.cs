using UnityEngine;

public class TreasureItem : MonoBehaviour
{
    public enum TreasureType { Coin, Gem, GoldBar, Trophy }
    public TreasureType type;

    [Range(1, 100)]
    public int value; // ���ؼ�ֵ

    [Tooltip("����͵ȡ�ļ���Ȩ�أ�Խ��Խ���ױ�͵")]
    public float weight = 1.0f;

    public bool IsCollected => value <= 0;
}
