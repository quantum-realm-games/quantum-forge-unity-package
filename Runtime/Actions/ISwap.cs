using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    [Serializable]
    public class ISwap : MonoBehaviour, IQuantumAction
    {
        [field: SerializeField] public QuantumProperty.Predicate[] Predicates { get; set; }

        [field: SerializeField] public QuantumProperty[] TargetProperties { get; set; }

        public float fraction = 1.0f;

        public void apply()
        {
            if (TargetProperties.Length != 2)
            {
                Debug.LogError("ISwap requires exactly 2 target properties");
                return;
            }
            Debug.Log($"Applying {fraction} ISWap to {TargetProperties[0]} and {TargetProperties[1]} with {Predicates.Length} predicates.");
            QuantumProperty.ISwap(TargetProperties[0], TargetProperties[1], fraction);
        }
    }

}
