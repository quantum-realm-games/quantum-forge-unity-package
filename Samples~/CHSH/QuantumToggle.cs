using UnityEngine;
using UnityEngine.UI;

namespace QRG.QuantumForge
{
    [RequireComponent(typeof(Toggle))]
    public class QuantumToggle : MonoBehaviour
    {
        public CHSHGame game;
        public Toggle toggle;

        void Start()
        {
            toggle.isOn = game.isQuantum;
            toggle.onValueChanged.AddListener(delegate { OnToggleChanged(); });
        }

        public void OnToggleChanged()
        {
            game.isQuantum = toggle.isOn;
            game.Reset();
        }
    }
}
