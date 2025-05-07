using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class SnakeDemo : MonoBehaviour
{
    [Header("目标")]
    public Transform target;
    public Transform exitTarget;
    public Transform treasureSpawnPoint;
    public TreasureChest targetChest;

    [Header("跳跃参数")]
    public float jumpDistance = 3f;
    public float jumpForce = 5f;
    public float exitTriggerDistance = 1.0f;

    [Header("宝藏偷取权重")]
    public List<TreasureItem> possibleItems;

    [Header("闪红反馈")]
    public Renderer flashRenderer;
    public Color flashColor = Color.red;
    public float flashSingleDuration = 0.1f;
    public int flashCount = 3;

    public AudioSource deathAudioSource;

    public string weaponTag = "Weapon";
    public float deathDelay = 3f;

    private Color originalColor;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Animator animator;

    private GameObject stolenTreasure;
    private bool isActivated = false;
    private bool hasStolen = false;
    private bool hasJumped = false;
    private bool isHit = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        animator = GetComponent<Animator>();
        originalColor = flashRenderer.material.color;
    }

    void Update()
    {
        if (!isActivated) return;

        if (hasStolen && !hasJumped)
        {
            agent.SetDestination(exitTarget.position);

            float distanceToExit = Vector3.Distance(transform.position, exitTarget.position);
            if (distanceToExit < exitTriggerDistance)
            {
                DoJump();
            }
        }
    }

    public void ActivateSnake()
    {
        if (isActivated) return;

        isActivated = true;
        agent.SetDestination(target.position);
        Debug.Log("Snake activated");
    }

    private void OnTriggerEnter(Collider other)
    {
        // 被武器攻击
        if (other.CompareTag(weaponTag))
        {
            Debug.Log("Snake hit by weapon");
            OnWeaponHit();
            return;
        }

        // 偷宝逻辑（仅激活后才触发）
        if (isActivated && !hasStolen && other.CompareTag("TreasureChest"))
        {
            StealFromChest();
            animator.SetTrigger("IsAttack");
        }
    }


    public void OnWeaponHit()
    {
        if (isHit) return;
        isHit = true;

        Debug.Log("Snake was hit");

        StartCoroutine(FlashRedMultiple());
        DropTreasure();

        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        FreezeMonster();
        Invoke(nameof(DestroySelf), deathDelay);
    }

    void StealFromChest()
    {
        hasStolen = true;

        TreasureItem selected = SelectTreasureWeighted();
        if (selected == null) return;

        if (targetChest != null)
        {
            targetChest.RemoveTreasure(selected.value);
        }

        if (treasureSpawnPoint != null)
        {
            stolenTreasure = Instantiate(
                selected.gameObject,
                treasureSpawnPoint.position,
                Quaternion.identity,
                treasureSpawnPoint
            );

            Rigidbody rb = stolenTreasure.GetComponent<Rigidbody>();
            Collider col = stolenTreasure.GetComponent<Collider>();
            col.enabled = false;
            rb.isKinematic = true;
            rb.useGravity = false;
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
        if (agent != null) agent.enabled = false;

        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

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

        if (animator != null)
        {
            animator.SetTrigger("IsDead");
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
