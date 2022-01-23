using UnityEngine;

namespace selection
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Character Selection/selection.Character")]
    public class Character : ScriptableObject
    {
        [SerializeField] private string characterName = default;
        [SerializeField] private GameObject characterPreviewPrefab = default;
        [SerializeField] private GameObject gameplayCharacterPrefab = default;

        public string CharacterName => characterName;
        public GameObject CharacterPreviewPrefab => characterPreviewPrefab;
        public GameObject GameplayCharacterPrefab => gameplayCharacterPrefab;
    }
}

