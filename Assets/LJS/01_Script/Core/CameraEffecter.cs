using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraEffecter : MonoSingleton<CameraEffecter>
{
    public CinemachineVirtualCamera currnetCam { get; private set; }

    private void Awake() {
        currnetCam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float power, float range, float time){
        StartCoroutine(ShakeCoro(power, range, time));
    }

    private IEnumerator ShakeCoro(float power, float range, float time)
    {
        CinemachineBasicMultiChannelPerlin perlin = 
            currnetCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = power;
        perlin.m_FrequencyGain = range;

        yield return new WaitForSeconds(time);

        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;
    }
}
