using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraAction : MonoBehaviour
{
    public static CameraAction Instance { get; private set; }

    private CinemachineVirtualCamera followCam;
    private CinemachineBasicMultiChannelPerlin bPerlin;
    private CinemachineFramingTransposer transposer;
    private Vector3 defaultFollowOffset;

    private Tween offsetTween;

    private bool isShake = false;
    private float currentTime = 0f; // ��鸮�� �ð�

    public RectTransform cinematic_top;
    public RectTransform cinematic_bottom;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("�ټ��� ī�޶� ��ũ��Ʈ�� �������Դϴ�.");
        }
        Instance = this;
        followCam = GetComponent<CinemachineVirtualCamera>();
        bPerlin = followCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        transposer = followCam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Start()
    {
        defaultFollowOffset = transposer.m_TrackedObjectOffset;
    }

    public static void ShakeCam(float intensity, float time)
    {
        // �ڷ�ƾ ȣ��
        if (!Instance.isShake)
        {
            Instance.isShake = true;
            Instance.StartCoroutine(Instance.ShakeUpdate(intensity, time));
        }
        else
        {
            Instance.bPerlin.m_AmplitudeGain = intensity;
            Instance.currentTime = time;
        }
    }

    public static void ZoomCam(float zoom, float time)
    {
        DOTween.To(() => Instance.followCam.m_Lens.OrthographicSize, value => Instance.followCam.m_Lens.OrthographicSize = value,
            zoom, time);
    }

    public static void OffsetCam(Vector3 offset, float time)
    {
        Instance.offsetTween.Kill();
        Instance.offsetTween = DOTween.To(() => Instance.transposer.m_TrackedObjectOffset, value => Instance.transposer.m_TrackedObjectOffset = value,
            offset, time).OnComplete(() =>
            {
                DOTween.To(() => Instance.transposer.m_TrackedObjectOffset, value => Instance.transposer.m_TrackedObjectOffset = value,
                Instance.defaultFollowOffset, 1);
            });
    }

    public static void CinematicBar(bool appear, float time) // ���Ʒ� ������ �� �ö���°�
    {
        if (appear)
        {
            Instance.cinematic_top.DOSizeDelta(new Vector2(Instance.cinematic_top.sizeDelta.x, 120), time);
            Instance.cinematic_bottom.DOSizeDelta(new Vector2(Instance.cinematic_bottom.sizeDelta.x, 120), time);
        }
        else
        {
            Instance.cinematic_top.DOSizeDelta(new Vector2(Instance.cinematic_top.sizeDelta.x, 0), time);
            Instance.cinematic_bottom.DOSizeDelta(new Vector2(Instance.cinematic_bottom.sizeDelta.x, 0), time);
        }
    }

    public IEnumerator ShakeUpdate(float intensity, float time)
    {
        bPerlin.m_AmplitudeGain = intensity;
        currentTime = 0;

        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                break;
            }
            bPerlin.m_AmplitudeGain = Mathf.Lerp(intensity, 0f, currentTime / time);
        }
        isShake = false;

        bPerlin.m_AmplitudeGain = 0;
    }
}