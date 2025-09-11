using UnityEngine;
using TMPro;

namespace QRG.QuantumForge
{
    public class ShowBit : MonoBehaviour
    {
        public CHSHReferee referee;
        public int bitIndex;

        void Update()
        {
            if (referee == null) return;
            var bit = bitIndex == 0 ? referee.X : referee.Y;
            GetComponent<TextMeshProUGUI>().text = bit.ToString();
        }
    }
}
