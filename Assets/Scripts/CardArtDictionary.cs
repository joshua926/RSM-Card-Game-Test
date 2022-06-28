using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardArtDictionary : ScriptableObject
{
    [SerializeField, NonReorderable] ImageIDPair[] imageIDs;
    Dictionary<int, Sprite> dictionary;
    
    public bool TryGetArt(int id, out Sprite art)
    {
        if (dictionary == null)
        {
            CreateDictionary();
        }
        return dictionary.TryGetValue(id, out art);
    }

    /// <summary>
    /// If ID does not exist then this will return null.
    /// </summary>
    public Sprite GetArt(int id)
    {
        if (dictionary == null)
        {
            CreateDictionary();
        }
        dictionary.TryGetValue(id, out var tex);
        return tex;
    }

    void CreateDictionary()
    {
        dictionary = new Dictionary<int, Sprite>(imageIDs.Length);
        if (imageIDs != null)
        {
            for (int i = 0; i < imageIDs.Length; i++)
            {
                var value = imageIDs[i];
                if (value != null && !dictionary.ContainsKey(value.id))
                {
                    dictionary.Add(value.id, value.texture);
                }
            }
        }
    }

    [System.Serializable]
    public class ImageIDPair
    {
        public int id;
        public Sprite texture;
    }
}
