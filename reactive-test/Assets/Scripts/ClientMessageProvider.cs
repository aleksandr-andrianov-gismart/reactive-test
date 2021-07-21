using UnityEngine;

public class ClientMessageProvider : MonoBehaviour
{
    public string GetMessageSecretCode(string key)
    {
        return key.GetHashCode().ToString();
    }
}
