using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using GeneralPurpose;

namespace RSMCardGame
{
    [CreateAssetMenu]
    public class CardArtDictionarySO : ScriptableObject
    {
        [SerializeField, NonReorderable] ImageIDPair[] imageIDs;
        Dictionary<int, Sprite> dictionary = new Dictionary<int, Sprite>();
        public int Count => dictionary.Count;

        void Awake()
        {
            Init();
        }

        void OnValidate()
        {
            Init();
        }

        public void Init()
        {
            dictionary.Clear();
            int length = imageIDs == null ? 0 : imageIDs.Length;
            for (int i = 0; i < length; i++)
            {
                var value = imageIDs[i];
                if (value != null && !dictionary.ContainsKey(value.id))
                {
                    dictionary.Add(value.id, value.sprite);
                }
            }
        }

        public bool ContainsID(int image_id)
        {
            return dictionary.ContainsKey(image_id);
        }

        public Sprite GetCardArt(int image_id)
        {
            Assert.IsTrue(Count != 0, $"Card Art Dictionary count is 0.");
            bool artExists = ContainsID(image_id);
            Assert.IsTrue(artExists, $"Card art with image_id {image_id} does not exist.");
            return dictionary[image_id];
        }

        [System.Serializable]
        public class ImageIDPair
        {
            public int id;
            public Sprite sprite;
        }
    }
}