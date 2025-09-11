using UnityEngine;

namespace QRG.QuantumForge
{
    public class CHSHPlayer : MonoBehaviour
    {
        public MeasurementWitness measure0;
        public MeasurementWitness measure1;

        public void Reset()
        {
            measure0.Initialize();
            measure1.Initialize();
        }

        public int Play(int x)
        {
            if(x == 0) return measure0.GetResult();
            else return measure1.GetResult();
        }
    }
}
