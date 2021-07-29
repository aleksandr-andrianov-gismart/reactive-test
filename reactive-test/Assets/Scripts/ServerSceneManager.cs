using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ServerSceneManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_explosionVFX = null;
    [SerializeField] private List<Light> m_pointLights = null;

    private IntReactiveProperty m_lightIndexRx = new IntReactiveProperty(0);

    private int m_deactivateLightIndex = 0;

    private void Start()
    {
        m_lightIndexRx.Subscribe(_ =>
        {
            m_deactivateLightIndex = m_lightIndexRx.Value == 0 ? m_deactivateLightIndex++ : m_lightIndexRx.Value - 1;

            if (m_lightIndexRx.Value >= m_pointLights.Count) m_lightIndexRx.Value = 0;

            SwitchLight();
        }).AddTo(this);
    }

    public void SwitchLight()
    {
        m_pointLights[m_deactivateLightIndex].enabled = false;
        m_pointLights[m_lightIndexRx.Value].enabled = true;
    }

    public void EmitParticles()
    {
        m_explosionVFX.Stop();
        m_explosionVFX.Play();
    }

    public void ChangeLightIndex()
    {
        m_lightIndexRx.Value++;
    }
}
