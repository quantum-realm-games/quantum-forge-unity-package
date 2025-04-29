using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    [Serializable]
    public class ISwap : MonoBehaviour, IQuantumAction
    {
        [Tooltip("Predicates that determine the conditions for this action.")]
        [field: SerializeField] public Predicate[] Predicates { get; set; }

        [Tooltip("Quantum properties that this action targets.")]
        [field: SerializeField] public QuantumProperty[] TargetProperties { get; set; }

        [Tooltip("Fraction of the ISwap operation to apply.")]
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
