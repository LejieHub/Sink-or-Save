using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CrabAI : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float moveSpeed = 2f;
    public Transform player;

    private bool isChasing = false;
    private bool isOnPlayer = false;
    private bool isGrabbed = false;

    private Transform xrOriginHead;
    private XRGrabInteractable grabInteractable;

    private float reactivationDelay = 3f; // ��������ȴʱ�䣨�ɵ���
    private float reactivateTimer = 0f;
    private bool isReactivating = false;

    private Animator crabAnimator;
    private AudioSource crabAudio;


    void Start()
    {
        if (player == null)
        {
            GameObject xrOrigin = GameObject.FindWithTag("Player");
            if (xrOrigin != null)
                player = xrOrigin.transform;
        }

        xrOriginHead = GameObject.FindWithTag("MainCamera")?.transform;
        grabInteractable = GetComponent<XRGrabInteractable>();

        crabAnimator = GetComponent<Animator>();

        // ����ץȡ�¼�
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }
    void Update()
    {
        //�ȴ�����ȴ�߼�����ʹ����״̬ return��Ҳ����ִ�У�
        if (isReactivating)
        {
            reactivateTimer -= Time.deltaTime;
            if (reactivateTimer <= 0f)
            {
                isReactivating = false;

                ResetRotation();
            }
        }

        //���ж��ܲ��ܽ���׷���߼�
        if (isOnPlayer || isGrabbed || isReactivating)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        isChasing = distance <= detectionRadius;

        if (isChasing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            direction.y = 0f; // ˮƽ��ת����������ͷ
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ƽ���������
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    private void ResetRotation()
    {
        // ����������ҵķ���
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0f; // ����ˮƽ���򣬱�����ͷ���ͷ

        // ���ó���
        transform.rotation = Quaternion.LookRotation(directionToPlayer);

        // ��ѡ���� Rigidbody �Ľ��ٶ����㣬��ֹ������ת
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOnPlayer && !isGrabbed)
        {
            AttachToPlayerFace();
        }
    }

    void AttachToPlayerFace()
    {
        isOnPlayer = true;

        transform.SetParent(xrOriginHead);
        transform.localPosition = new Vector3(0, 0, 0.3f);
        transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = true;

        if (crabAnimator != null)
            crabAnimator.enabled = true;

        crabAudio = GetComponent<AudioSource>();
        if (crabAudio != null && !crabAudio.isPlaying)
            crabAudio.Play();
    }



    void OnGrabbed(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        isOnPlayer = false;

        transform.SetParent(null);

        if (crabAnimator != null)
            crabAnimator.enabled = false;

        if (crabAudio != null && crabAudio.isPlaying)
            crabAudio.Stop();
    }

    void OnReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;

        isReactivating = true;
        reactivateTimer = reactivationDelay;

        if (crabAnimator != null)
            crabAnimator.enabled = false;

        if (crabAudio != null && crabAudio.isPlaying)
            crabAudio.Stop();
    }


}