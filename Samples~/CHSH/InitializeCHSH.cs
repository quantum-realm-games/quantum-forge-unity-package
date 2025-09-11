using UnityEngine;
using QRG.QuantumForge.Runtime;

namespace QRG.QuantumForge
{

    public class InitializeCHSH : MonoBehaviour
    {
        public QuantumProperty coin1;
        public MeasurementWitness coin1Measurement0;
        public MeasurementWitness coin1Measurement1;
        public QuantumProperty coin2;
        public MeasurementWitness coin2Measurement0;
        public MeasurementWitness coin2Measurement1;

        public void InitializeClassical()
        {
            ResetQuantumProperty(coin1);
            ResetQuantumProperty(coin2);

            coin1.Hadamard();
            coin2.Hadamard();


            coin1Measurement0.Initialize();
            coin1Measurement1.Initialize();
            coin2Measurement0.Initialize();
            coin2Measurement1.Initialize();
        }

        public void InitializeEntangled()
        {
            ResetQuantumProperty(coin1);
            ResetQuantumProperty(coin2);

            coin1.Hadamard();
            coin2.Shift(coin1.is_value(0)); // entangle coin2 with coin1 opposite

            coin1Measurement0.Initialize();
            coin1Measurement1.Initialize();
            coin2Measurement0.Initialize();
            coin2Measurement1.Initialize();
        }

        private void ResetQuantumProperty(QuantumProperty q)
        {
            var result = QuantumProperty.Measure(q);
            if (result[0] == 1) q.Shift();
        }
    }
}
