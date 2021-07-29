using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ClientInputHandler : MonoBehaviour
{
    [SerializeField] private Button m_lightSwitcherButton = null;
    [SerializeField] private Button m_emitParticlesButton = null;

    [SerializeField] private ClientConnectionController m_clientConnectionController = null;

    private string m_lightMessageKey = "light";
    private string m_explodeMessageKey = "explode";

    private void Start()
    {
        m_lightSwitcherButton.onClick.AsObservable().Subscribe(_ =>
            m_clientConnectionController.SendMessageToServer(
                ClientMessageProvider.GetMessageSecretCode(m_lightMessageKey))
        );
        m_emitParticlesButton.onClick.AsObservable().Subscribe(_ =>
            m_clientConnectionController.SendMessageToServer(
                ClientMessageProvider.GetMessageSecretCode(m_explodeMessageKey))
        );
    }
}
