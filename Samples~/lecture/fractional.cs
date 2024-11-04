using System.Collections;
using System.Collections.Generic;
using QRG.QuantumForge.Runtime;
using UnityEngine;
using QuantumProperty = QRG.QuantumForge.Runtime.QuantumProperty;

namespace QRG.QuantumForge
{
    public class fractional : MonoBehaviour
    {
        public QuantumProperty p1;
        public QuantumProperty p2;
        public float fraction;

        public void apply()
        {
            QuantumProperty.FractionalISwap(p1,p2,fraction);
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
