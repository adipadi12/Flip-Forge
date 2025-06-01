using Unity.Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineCamera cinemachineCamera;

    public static CinemachineShake Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ShakeCamera(float intensity)
    {
        CinemachineBasicMultiChannelPerlin perlin = cinemachineCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

        perlin.AmplitudeGain = intensity;
    }
}
