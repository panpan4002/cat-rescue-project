using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CameraShaker(float amplitude, float frequency, float time)
    {
        StartCoroutine(Shake(amplitude, frequency, time));
    }

    IEnumerator Shake(float amplitude, float frequency, float time)
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

        yield return new WaitForSeconds(time);

        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
    }

    public void CameraDutch(float dutch, float time)
    {
        StartCoroutine(Dutch(dutch, time));
    }

    IEnumerator Dutch(float dutch, float time)
    {   
        virtualCamera.m_Lens.Dutch = dutch;
        
        yield return new WaitForSeconds(time);

        virtualCamera.m_Lens.Dutch = 0;
    }
}
