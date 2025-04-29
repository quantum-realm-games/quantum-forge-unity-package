//# Copyright 2024 Quantum Realm Games
//#
//# Licensed under the Quantum Realm Games Quantum Forge Unity Toolkit 
//# license, Version 1.0 (the "License"); you may not use this file 
//# except in compliance with the License.
//# You may obtain a copy of the License at
//#
//#     https://www.quantumrealmgames.com/quantum_forge_toolkit_license
//#
//# Unless required by applicable law or agreed to in writing, software
//# distributed under the License is distributed on an "AS IS" BASIS,
//# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//# See the License for the specific language governing permissions and
//# limitations under the License.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    /// <summary>
    /// Represents a quantum action that applies an NCycle operation to quantum properties.
    /// </summary>
    [Serializable]
    public class NCycle : MonoBehaviour, IQuantumAction
    {
        /// <summary>
        /// Gets or sets the predicates that determine the conditions for this action.
        /// </summary>
        [Tooltip("Predicates that determine the conditions for this action.")]
        [field: SerializeField] public Predicate[] Predicates { get; set; }

        /// <summary>
        /// Gets or sets the quantum properties that this action targets.
        /// </summary>
        [Tooltip("Quantum properties that this action targets.")]
        [field: SerializeField] public QuantumProperty[] TargetProperties { get; set; }

        /// <summary>
        /// Applies the NCycle operation to the target quantum properties.
        /// </summary>
        public void apply()
        {
            if (TargetProperties.Length != 2)
            {
                Debug.LogError("Must supply exactly 2 QuantumProperties to NCycle");
            }
            else
            {
                QuantumProperty.NCycle(TargetProperties[0], TargetProperties[1]);
            }
        }

    }

}
