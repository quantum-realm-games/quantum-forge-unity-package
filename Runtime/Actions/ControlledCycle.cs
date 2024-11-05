using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    [System.Serializable]
    public class ControlledCycle : MonoBehaviour, IQuantumAction
    {
        public string ActionName => "ControlledCycle";

        [SerializeField] private QuantumProperty[] _targetProperties;
        public QuantumProperty[] TargetProperties => _targetProperties;

        public int controlValue = 0;

        public void apply()
        {
            apply(TargetProperties);
        }

        public void apply(params QuantumProperty[] targetProperties)
        {
            if (targetProperties.Length != 2)
            {
                Debug.LogError("Must supply exactly 2 QuantumProperties to ControlledCycle");
            }
            else
            {
                QuantumProperty.Cycle(TargetProperties[1], TargetProperties[0].is_value(controlValue));
            }
        }
    }

}

