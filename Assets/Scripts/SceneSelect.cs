using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

namespace UnityEngine.XR.ARFoundation.h1j1k1
{
    public class SceneSelect : MonoBehaviour
    {
        [SerializeField]
        Scrollbar m_HorizontalScrollBar;
        public Scrollbar horizontalScrollBar
        {
            get => m_HorizontalScrollBar;
            set => m_HorizontalScrollBar = value;
        }

        [SerializeField]
        Scrollbar m_VerticalScrollBar;
        public Scrollbar verticalScrollBar
        {
            get => m_VerticalScrollBar;
            set => m_VerticalScrollBar = value;
        }

        [SerializeField]
        GameObject m_AllMenu;
        public GameObject allMenu
        {
            get => m_AllMenu;
            set => m_AllMenu = value;
        }

        void Start()
        {
            ScrollToStartPosition();
        }

        static void LoadScene(string sceneName)
        {
            LoaderUtility.Initialize();
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public void AutoGenButtonPressed()
        {
            LoadScene("AutoGen");
        }
        public void FaceGenButtonPressed()
        {
            LoadScene("FaceGen");
        }
        public void TapAndGensButtonPressed()
        {
            LoadScene("TapAndGen");
        }
        void ScrollToStartPosition()
        {
            m_HorizontalScrollBar.value = 0;
            m_VerticalScrollBar.value = 1;
        }
    }
}