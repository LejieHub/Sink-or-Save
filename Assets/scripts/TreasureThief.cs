using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using System.Collections;

public class TreasureThief : MonoBehaviour
{
    [Header("目标")]
    public Transform target;       // 宝箱位置
    public Transform exitTarget;   // 跳海点位置
    public Transform treasureSpawnPoint; // 宝藏生成在怪物嘴里的位置
    public TreasureChest targetChest;    // 被偷的宝箱

    [Header("跳跃参数")]
    public float jumpDistance = 3f;
    public float jumpForce = 5f;
    public float exitTriggerDistance = 1.0f;

    [Header("宝藏偷取权重")]
    public List<TreasureItem> possibleItems;

    [Header("闪红反馈")]
    public Renderer flashRenderer; //拖入怪物的 MeshRenderer 或 SkinnedMeshRenderer
    public Color flashColor = Color.red;
    public float flashSingleDuration = 0.1f;
    public int flashCount = 3;

    private Color originalColor;
    private bool isFlashing = false;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private bool hasStolen = false;
    private bool hasJumped = false;
    private GameObject stolenTreasure;

    public string weaponTag = "Weapon"; // 确保刀设置了这个 Tag
    public float deathDelay = 3f; // 延迟几秒消失
    private bool isHit = false;
    private Animator animator;

    [Header("音效")]
    public AudioSource deathAudioSource; // 记得在 Inspector 拖入子物体上的 AudioSource



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        animator = GetComponent<Animator>();
        // 前往宝箱
        agent.SetDestination(target.position);

        originalColor = flashRenderer.material.color;

    }

    void Update()
    {
        if (hasStolen && !hasJumped)
        {
            // 前往出口点
            agent.SetDestination(exitTarget.position);

            float distanceToExit = Vector3.Distance(transform.position, exitTarget.position);
            if (distanceToExit < exitTriggerDistance)
            {
                DoJump();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasStolen && other.CompareTag("TreasureChest"))
        {
            StealFromChest();
            animator.SetTrigger("IsAttack");
        }
    }

    void StealFromChest()
    {
        Debug.Log("宝物被偷了！");
        hasStolen = true;

        TreasureItem selected = SelectTreasureWeighted();
        if (selected == null)
        {
            Debug.LogWarning("没有选中宝藏，可能是权重问题");
            return;
        }

        Debug.Log($"偷取的宝藏类型：{selected.type}, 价值：{selected.value}, 权重：{selected.weight}");

        if (targetChest != null)
        {
            Debug.Log($"宝箱原始价值：{targetChest.currentValue}");
            targetChest.RemoveTreasure(selected.value);
            Debug.Log($"宝箱新价值：{targetChest.currentValue}");
        }
        else
        {
            Debug.LogWarning("targetChest 没有被赋值！");
        }

        if (treasureSpawnPoint != null)
        {
            stolenTreasure = Instantiate(selected.gameObject, treasureSpawnPoint.position, Quaternion.identity, treasureSpawnPoint);
            Rigidbody rb = stolenTreasure.GetComponent<Rigidbody>();
            Collider col = stolenTreasure.GetComponent<Collider>();
            col.enabled = false;
            rb.isKinematic = true;
            rb.useGravity = false;

            Debug.Log("宝藏成功生成在嘴里！");
        }
        else
        {
            Debug.LogWarning("未设置宝藏生成点！");
        }
    }


    TreasureItem SelectTreasureWeighted()
    {
        float totalWeight = 0f;
        foreach (var item in possibleItems)
            totalWeight += item.weight;

        float rand = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var item in possibleItems)
        {
            cumulative += item.weight;
            if (rand <= cumulative)
                return item;
        }

        return null;
    }


    void DoJump()
    {
        hasJumped = true;
        agent.enabled = false;
        rb.isKinematic = false;

        Vector3 jumpVelocity = transform.forward * jumpDistance + Vector3.up * jumpForce;
        rb.linearVelocity = jumpVelocity;

        Debug.Log("怪物跳海逃走了！");
    }

    public void OnWeaponHit()
    {
        if (isHit) return;
        isHit = true;

        Debug.Log("怪物被击中（来自 Trigger 子物体）");

        StartCoroutine(FlashRedMultiple());

        DropTreasure();

        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Hit");
        }

        FreezeMonster();
        Invoke(nameof(DestroySelf), deathDelay);
    }

    IEnumerator FlashRedMultiple()
    {
        if (flashRenderer == null)
            yield break;

        for (int i = 0; i < flashCount; i++)
        {
            flashRenderer.material.color = flashColor;
            yield return new WaitForSeconds(flashSingleDuration);

            flashRenderer.material.color = originalColor;
            yield return new WaitForSeconds(flashSingleDuration);
        }
    }


    void ResetColor()
    {
        if (flashRenderer != null)
        {
            flashRenderer.material.color = originalColor;
        }

        isFlashing = false;
    }


    void DropTreasure()
    {
        if (stolenTreasure != null)
        {
            stolenTreasure.transform.SetParent(null);

            Rigidbody rb = stolenTreasure.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            Collider[] colliders = stolenTreasure.GetComponentsInChildren<Collider>();
            foreach (var col in colliders)
            {
                col.enabled = true;
            }

            stolenTreasure = null;
        }
    }

    void FreezeMonster()
    {
        Debug.Log("怪物被冻结");

        // 停止导航
        if (agent != null) agent.enabled = false;

        // 停止物理
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 禁用怪物自身碰撞器
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }

        if (deathAudioSource != null && deathAudioSource.clip != null)
        {
            deathAudioSource.Play();
            Destroy(gameObject, deathAudioSource.clip.length);
        }
        else
        {
            Destroy(gameObject, deathDelay);
        }


        // 可选：播放动画 / 改颜色
        animator.SetTrigger("IsDead");
    }

    void DestroySelf()
    {
        Debug.Log("怪物消失了！");
        Destroy(gameObject);
    }



}
