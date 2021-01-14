using UnityEngine;
using UnityEngine.UI;

namespace GUI_Scripts
{
    public class ProgressBar : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        void Start()
        {
        }
  
        void Update()
        {
            SetProgress(GameManager.Instance.GetProgress());
        }

        public void SetProgress(float progress)
        {

            _slider.value = progress;
            
        }
    }
}
