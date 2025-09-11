using System;
using UnityEngine;
using QRG.QuantumForge.Runtime;

namespace QRG.QuantumForge
{

    [RequireComponent(typeof(QuantumProperty))]
    public class MeasurementWitness : MonoBehaviour
    {
        public float measurementAngle= 0;
        public QuantumProperty coin;
        private QuantumProperty witness;
        public int lastResult = -1;

        public void Initialize()
        {
            //// Note, assumes coins have been reset already. Can have entanglement consequences otherwise.
            //witness = GetComponent<QuantumProperty>();
            //var value = QuantumProperty.Measure(witness);
            //if (value[0] == 1) witness.Shift();

            //// we use a measurement ancilla to leave the system undisturbed, for probability tracking.
            //coin.Clock(0.5f);
            //coin.Shift( measurementAngle/(Mathf.PI));
            //coin.Clock(-0.5f);

            //witness.Shift(1.0f, coin.is_value(1));

            //coin.Clock(0.5f);
            //coin.Shift(-measurementAngle / (Mathf.PI));
            //coin.Clock(-0.5f);
        }

        public void SetMeasurementAngle(float angle)
        {
            measurementAngle = angle;
            Debug.Log("Measurement angle set to " + angle);
        }

        public void Measure()
        {
            coin.Clock(0.5f);
            coin.Shift(measurementAngle / (Mathf.PI));
            coin.Clock(-0.5f);
            lastResult = QuantumProperty.Measure(coin)[0];
        }

        public int GetResult()
        {
            Measure();
            return lastResult;
        }
        
    }
}
