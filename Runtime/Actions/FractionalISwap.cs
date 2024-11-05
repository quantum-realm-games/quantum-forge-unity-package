using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    [Serializable]
    public class FractionalISwap : MonoBehaviour, IQuantumAction
    {

        public string ActionName => "FractionalISwap";
        [SerializeField] private QuantumProperty[] _targetProperties;
        public QuantumProperty[] TargetProperties => _targetProperties;
        public float fraction;

        public void apply()
        {
            apply(TargetProperties);
        }

        public void apply(params QuantumProperty[] targetProperties)
        {
            if(targetProperties.Length != 2)
            {
                Debug.LogError("FractionalISwap requires exactly 2 target properties");
                return;
            }
            QuantumProperty.FractionalISwap(targetProperties[0], targetProperties[1], fraction);
        }
    }

}
