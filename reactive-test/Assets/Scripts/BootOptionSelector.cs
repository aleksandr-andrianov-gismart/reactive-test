using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootOptionSelector : MonoBehaviour
{
    [SerializeField] private Button m_startServerButton = null;
    [SerializeField] private Button m_startClientButton = null;

    private string m_serverSceneName = "Server";
    private string m_clientSceneName = "Client";

    private void Start()
    {
        m_startServerButton.onClick.AsObservable().Subscribe(_ => LoadSceneByName(m_serverSceneName));
        m_startClientButton.onClick.AsObservable().Subscribe(_ => LoadSceneByName(m_clientSceneName));
    }

    private void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
