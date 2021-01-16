﻿using UnityEngine;
using UnityEngine.UI;

namespace GUI_Scripts
{
    public class ProgressBar : MonoBehaviour
    {
        private Slider _slider;
        private Progress _progress;
        private RectTransform _rectTransform;
        public GameObject stagePointImagePrefab;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        void Start()
        {
            LoadComponents();
            Invoke("RenderStageIndicators", 0.01f);
        }
  
        void Update()
        {
            SetProgress(GameManager.Instance.GetProgress());
        }

        private void LoadComponents()
        {
            _progress = GameObject.Find("LandWrapper").GetComponent<Progress>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void RenderStageIndicators()
        {
            GameObject[] stagePoints = GameObject.FindGameObjectsWithTag("StagePoint");
     
            for(var i = 0 ; i < stagePoints.Length ; i ++)
            {
                float stagePointPositionX = _progress.CalcHowFarObjectIsFromInitialPosition(stagePoints[i].transform.position.x);
                float gameWidth = _progress.GetGameWidth();
                float progress = stagePointPositionX / gameWidth;
                float progressBarMaxPosition = _rectTransform.rect.xMax;
                float progressBarMinPosition = _rectTransform.rect.xMin;
                float progressBarWidth = progressBarMaxPosition - progressBarMinPosition;
                float stagePointMarkPosition = progressBarMinPosition + (progressBarWidth * progress);
                GameObject stagePointImage = Instantiate(stagePointImagePrefab, Vector3.zero, Quaternion.identity);
                stagePointImage.transform.SetParent(transform, false);
                stagePointImage.transform.localScale = new Vector3(1, 1, 1);
                stagePointImage.transform.position = new Vector3(transform.position.x + stagePointMarkPosition,
                    stagePointImage.transform.position.y, stagePointImage.transform.position.z);
            }
        }

        public void SetProgress(float progress)
        {

            _slider.value = progress;
            
        }
    }
}