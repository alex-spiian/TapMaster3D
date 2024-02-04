using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class LevelsView : MonoBehaviour
    {
        [SerializeField] private LevelsViewConfig _levelsViewConfig;
        [SerializeField] private Image[] _LevelsImages;

        public void UpdateLevelsView()
        {
            SetCompletedLevelImage();
            SetCurrentLevelImage();
            SetNotCompletedLevelImage();
        }
        
        public void ResetLevels()
        {
            for (int i = 0; i < _LevelsImages.Length; i++)
            {
                _LevelsImages[i].sprite = _levelsViewConfig.NotCompletedLevelSprite;
            }
        }
        private void SetCompletedLevelImage()
        {

            for (int i = 0; i < PlayerPrefs.GetInt(GlobalConstants.CurrentLevel); i++)
            {
                var currentLevel = i + 1;
                _LevelsImages[i].sprite = _levelsViewConfig.CompletedLevelSprite;
                var textLabel = _LevelsImages[i].GetComponentInChildren<TextMeshProUGUI>();
                textLabel.text = currentLevel.ToString();
            }
        }
        
        private void SetNotCompletedLevelImage()
        {
            var currentLevelIndex = PlayerPrefs.GetInt(GlobalConstants.CurrentLevel);
            
            for (int i = currentLevelIndex + 1; i < _LevelsImages.Length; i++)
            {
                _LevelsImages[i].sprite = _levelsViewConfig.NotCompletedLevelSprite;
            }
        }
        
        private void SetCurrentLevelImage()
        {
            var currentLevelIndex = PlayerPrefs.GetInt(GlobalConstants.CurrentLevel);
            
            _LevelsImages[currentLevelIndex].sprite = _levelsViewConfig.CurrentLevelSprite;
        }
        

    }
}