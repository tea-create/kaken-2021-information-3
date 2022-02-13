using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityEngine.XR.ARFoundation.h1j1k1.Auto
{
[RequireComponent(typeof(ARSessionOrigin))]
    public class Balancer : MonoBehaviour{

        AutoGen m_AutoGen;

        [SerializeField]
        Slider m_Slider;
        public Slider slider
        {
            get { return m_Slider; }
            set { m_Slider = value; }
        }

        [SerializeField]
        public float m_Min = 0f;
        public float min
        {
            get { return m_Min; }
            set { m_Min = value; }
        }

        [SerializeField]
        public float m_Max = 360f;

        public float max
        {
            get { return m_Max; }
            set { m_Max = value; }
        }

    [SerializeField]
        Text m_Text;
        public Text text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        int counter
        {
            get
            {
                return m_AutoGen.counter;
            }
            set
            {
                m_AutoGen.counter = value;
                UpdateText();
            }
        }

        public void OnSliderValueChanged()
        {
            if (slider != null)
                counter = (int) (slider.value * (max - min) + min);
        }

        void Awake()
        {
            m_AutoGen = GetComponent<AutoGen>();
        }

        void OnEnable()
        {
            if (slider != null)
                slider.value = (counter - min) / (max - min);
        }
        void UpdateText()
        {
            if (m_Text != null)
                m_Text.text = "v: " + counter;
        }
    }
}