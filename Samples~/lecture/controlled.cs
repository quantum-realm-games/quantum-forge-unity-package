using System.Collections;
using System.Collections.Generic;
using QRG.QuantumForge.Runtime;
using UnityEngine;
using QuantumProperty = QRG.QuantumForge.Runtime.QuantumProperty;

namespace QRG.QuantumForge.sample
{
    public class controlled : MonoBehaviour
    {
        public QuantumProperty control;
        public QuantumProperty target;
        public int value;

        public void apply()
        {
            QuantumProperty.Cycle(target,control.is_value(value));
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
