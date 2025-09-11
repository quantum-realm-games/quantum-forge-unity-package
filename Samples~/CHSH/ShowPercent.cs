using TMPro;
using UnityEngine;

namespace QRG.QuantumForge
{
    public class ShowPercent : MonoBehaviour
    {
        public CHSHGame game;

        // Update is called once per frame
        void Update()
        {
            if (game.rounds > 0)
            {
                float percent = (float)game.wins / game.rounds * 100f;
                GetComponent<TextMeshProUGUI>().text = $"Wins: {game.wins} / {game.rounds} ({percent:0.00}%)";
            }
        }
    }
}
