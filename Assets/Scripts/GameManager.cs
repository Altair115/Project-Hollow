using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    /// <summary>
    /// Manages the game on a higher level
    /// </summary>
    [AddComponentMenu("Mythirial/Game Manager")]
    public class GameManager : Singleton<GameManager>
    {
        public GameObject LoadingScreen;
        public Slider ProgressBar;
        public TextMeshProUGUI LoadingText;

        //Optional Settings
        //Background Changing
        //public Sprite[] Backgrounds;
        //public Image Background;

        //Tips
        public TextMeshProUGUI TipsText;
        public CanvasGroup AlphaCanvas;
        [SerializeField][TextArea(3, 10)] private string[] _tips;

        private GameManager() { }

        private void Awake()
        {
            SceneManager.LoadSceneAsync((int) SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
        }

        List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();

        public void LoadGame()
        {
            //Background.sprite = Backgrounds[Random.Range(0, Backgrounds.Length)];
            LoadingScreen.gameObject.SetActive(true);
            _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
            _scenesLoading.Add(SceneManager.LoadSceneAsync((int) SceneIndexes.TEST_DESERT, LoadSceneMode.Additive));

            StartCoroutine(GetLoadProgress());
        }


        private float _totalSceneLoadingProgress;
        /// <summary>
        /// Gets the progress for loading the scene
        /// </summary>
        /// <returns>scene loading progress</returns>
        public IEnumerator GetLoadProgress()
        {
            foreach (var scene in _scenesLoading)
            {
                while (!scene.isDone)
                {
                    _totalSceneLoadingProgress = 0;
                    foreach (var operation in _scenesLoading)
                    {
                        _totalSceneLoadingProgress += operation.progress;
                    }

                    _totalSceneLoadingProgress = (_totalSceneLoadingProgress / _scenesLoading.Count) * 100f;
                    LoadingText.text = $"Loading Environments: {_totalSceneLoadingProgress}%";
                    ProgressBar.value = Mathf.RoundToInt(_totalSceneLoadingProgress);
                    yield return null;
                }
            }

            LoadingScreen.gameObject.SetActive(false);
        }

        ///Pseudo Code for later initialisation progress tracking
        //public IEnumerator GetTotalProgress()
        //{
        //    float totalProgress = 0;
        //
        //    while (expression)
        //    {
        //        if (expr)
        //        {
        //            //etc = 0 
        //        }
        //        else
        //        {
        //            initialisation of etc Mathf.Round(etc's current value * 100f);
        //            LoadingText.text = $"Loading etc: {etc's total progress}%";  
        //        }
        //          
        //          totalProgress = Mathf.Round(_totalSceneLoadingProgress + etc) / 2f
        //          ProgressBar.value = Mathf.RoundToInt(_totalSceneLoadingProgress);
        //    }
        //    
        //}
        private int _tipCount;
        public IEnumerator GenerateTips()
        {
            _tipCount = Random.Range(0, _tips.Length);
            TipsText.text = _tips[_tipCount];
            while (LoadingScreen.activeInHierarchy)
            {
                yield return new WaitForSeconds(3f);
                AlphaCanvas.alpha = 0f;
                yield return new WaitForSeconds(.5f);
                _tipCount++;

                if (_tipCount >= _tips.Length)
                    _tipCount = 0;

                TipsText.text = _tips[_tipCount];
                AlphaCanvas.alpha = 1;
            }
        }
    }
}
