using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    [Serializable]
    public class Swap : MonoBehaviour, IQuantumAction
    {

        public string ActionName => "Swap";
        [SerializeField] private QuantumProperty[] _targetProperties;
        public QuantumProperty[] TargetProperties => _targetProperties;

        public void apply()
        {
            apply(TargetProperties);
        }

        public void apply(params QuantumProperty[] targetProperties)
        {
            if(targetProperties.Length != 2)
            {
                Debug.LogError("Swap requires exactly 2 target properties");
                return;
            }
            QuantumProperty.Swap(targetProperties[0], targetProperties[1]);
        }
    }

}
