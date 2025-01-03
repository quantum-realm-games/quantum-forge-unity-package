using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    [Serializable]
    public class Swap : MonoBehaviour, IQuantumAction
    {
        [field: SerializeField] public Predicate[] Predicates { get; set; }
        [field: SerializeField] public QuantumProperty[] TargetProperties { get; set; }

        public void apply()
        {
            if (TargetProperties.Length != 2)
            {
                Debug.LogError("Swap requires exactly 2 target properties");
                return;
            }
            QuantumProperty.Swap(TargetProperties[0], TargetProperties[1], Predicates);
        }
    }

}
