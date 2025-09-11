using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QRG.QuantumForge
{
    public class SliderValueDisplay : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI valueText;

        void Start()
        {
            if (slider != null)
            {
                slider.onValueChanged.AddListener(delegate { UpdateValueText(); });
                UpdateValueText(); // Initialize the text at start
            }
        }

        public void UpdateValueText()
        {
            if (slider != null && valueText != null)
            {
                valueText.text = slider.value.ToString("F0");
            }
        }
    }
}
