using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QRG.QuantumForge
{
    public class MeasurementUI : MonoBehaviour
    {
        public MeasurementWitness witness;
        public DialRotationHandler dial;
        public CHSHGame gameManager;
        public CHSHReferee referee;
        public TextMeshProUGUI resultText;

        public bool isPlayerA = true;
        public int refereeBit = 0;

        void OnEnable()
        {
            dial.OnAngleChanged += (rotation) =>
            {
                if (witness != null)
                {
                    witness.SetMeasurementAngle(rotation.eulerAngles.z*Mathf.PI/180);
                    gameManager.Initialize();
                }
            };
        }

        void Update()
        {
            GetComponent<Image>().enabled = (referee.Y == refereeBit && !isPlayerA) || (referee.X == refereeBit && isPlayerA);
            if (resultText != null)
            {
                resultText.text = "Result: " + witness.lastResult;
            }
        }

    }
}
