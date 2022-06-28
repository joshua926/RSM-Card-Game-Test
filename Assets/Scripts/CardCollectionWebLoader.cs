using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Assertions;

public class CardCollectionWebLoader : MonoBehaviour
{
    [Header("Loading cards from url: " + defaultURL)]
    public CardCollection cardCollection;
    public bool IsLoaded { get; private set; } = false;
    public const string defaultURL = "https://client.dev.kote.robotseamonster.com/TEST_HARNESS/json_files/cards.json";

    void Awake()
    {
        if (cardCollection)
        {
        StartCoroutine(Request(defaultURL, cardCollection));
        }
    }

    IEnumerator Request(string url, CardCollection cardCollection)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            Assert.IsTrue(webRequest.result == UnityWebRequest.Result.Success); // conditionally included only in dev builds

            switch (webRequest.result)
            {                
                case UnityWebRequest.Result.Success:
                    cardCollection.LoadFromJSON(webRequest.downloadHandler.text);
                    IsLoaded = true;
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogWarning("Unity web request failed: Connection Error");                    
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning("Unity web request failed: Protocol Error");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogWarning("Unity web request failed: Data Processing Error");
                    break;
                default:
                    Debug.LogWarning("Unity web request failed: Unknown Cause");
                    break;
            }
        }
    }
}
