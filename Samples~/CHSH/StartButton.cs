using UnityEngine;
using UnityEngine.UI;

namespace QRG.QuantumForge
{
    public class StartButton : MonoBehaviour
    {
        public Slider slider;
        public CHSHGame game;

        public void RunRounds()
        {
            game.Go((int)slider.value);
        }
    }
}
