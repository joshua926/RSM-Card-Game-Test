using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Assertions;
using GeneralPurpose;

namespace RSMCardGame
{
    public class CardDictionaryWebLoaderMB : MonoBehaviour
    {
        public UnityWebRequest.Result Result { get; private set; } = UnityWebRequest.Result.InProgress;
        public bool IsRequestCompleted => Result != UnityWebRequest.Result.InProgress;

        public void StartWebRequest(string url, CardDictionarySO cardDictionary, System.Action actionWhenComplete = null)
        {
            Assert.IsTrue(cardDictionary, "Given CardDictionary asset is null.");
            StartCoroutine(Request(url, cardDictionary, actionWhenComplete));
        }

        IEnumerator Request(string url, CardDictionarySO cardDictionary, System.Action actionWhenComplete)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                Result = UnityWebRequest.Result.InProgress;
                yield return webRequest.SendWebRequest();
                Result = webRequest.result;

                Assert.IsTrue(webRequest.result == UnityWebRequest.Result.Success); // conditionally included only in dev builds

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.Success:
                        cardDictionary.LoadFromJSON(webRequest.downloadHandler.text);
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

                if (actionWhenComplete != null)
                {
                    actionWhenComplete();
                }
            }
        }
    }
}