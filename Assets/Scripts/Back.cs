using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityEngine.XR.ARFoundation.h1j1k1
{
    public class Back : MonoBehaviour
    {
        [SerializeField]
        GameObject m_BackButton;
        public GameObject backButton
        {
            get => m_BackButton;
            set => m_BackButton = value;
        }

        void Start()
        {
            if (Application.CanStreamedLevelBeLoaded("Menu"))
            {
                m_BackButton.SetActive(true);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButtonPressed();
            }
        }

        public void BackButtonPressed()
        {
            if (Application.CanStreamedLevelBeLoaded("Menu"))
            {
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                LoaderUtility.Deinitialize();
            }
        }
    }
}