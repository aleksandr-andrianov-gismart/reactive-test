using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ClientInputHandler : MonoBehaviour
{
    [SerializeField] private Button m_lightSwitcherButton = null;
    [SerializeField] private Button m_emitParticlesButton = null;

    [SerializeField] private ClientConnectionController m_clientConnectionController = null;
    [SerializeField] private ClientMessageProvider m_clientMessageProvider = null;

    private string m_lightMessageKey = "light";
    private string m_explodeMessageKey = "explode";

    private void Start()
    {
        m_lightSwitcherButton.onClick.AsObservable().Subscribe(_ =>
            m_clientConnectionController.SendMessageToServer(
                m_clientMessageProvider.GetMessageSecretCode(m_lightMessageKey))
        );
        m_emitParticlesButton.onClick.AsObservable().Subscribe(_ =>
            m_clientConnectionController.SendMessageToServer(
                m_clientMessageProvider.GetMessageSecretCode(m_explodeMessageKey))
        );
    }
}
