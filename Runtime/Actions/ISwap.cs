using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    [Serializable]
    public class ISwap : MonoBehaviour, IQuantumAction
    {

        public string ActionName => "ISwap";
        [SerializeField] private QuantumProperty[] _targetProperties;
        public QuantumProperty.Predicate[] Predicates { get; }
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
                Debug.LogError("ISwap requires exactly 2 target properties");
                return;
            }
            QuantumProperty.ISwap(targetProperties[0], targetProperties[1], fraction);
        }
    }

}
