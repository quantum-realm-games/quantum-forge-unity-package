// Copyright (c) 2025 Quantum Realm Games, Inc. All rights reserved.
// See LICENSE.md for license information.

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRG.QuantumForge.Runtime;
using Unity.VisualScripting;

namespace QRG.QuantumForge.Runtime
{
    /// <summary>
    /// Tracks basis probabilities of specified quantum properties.
    /// </summary>
    public class ProbabilityTracker : MonoBehaviour
    {
        /// <summary>
        /// Quantum properties to track probabilities for.
        /// </summary>
        [Tooltip("Quantum properties to track probabilities for.")]
        [SerializeField] private QuantumProperty[] quantumProperties;

        /// <summary>
        /// Array representing the basis probabilities of the quantum properties.
        /// </summary>
        [Tooltip("Array representing the basis probabilities of the quantum properties.")]
        [SerializeField] private QuantumProperty.BasisProbability[] _probabilities;
        public QuantumProperty.BasisProbability[] Probabilities
        {
            get
            {
                _probabilities = GetBasisProbabilities();
                return _probabilities;
            }
        } 

        /// <summary>
        /// Indicates whether the probabilities should be updated continuously.
        /// </summary>
        [Tooltip("Indicates whether the probabilities should be updated continuously.")]
        [SerializeField] private bool continuous = true;

        /// <summary>
        /// Initializes the tracker and ensures quantum properties are set.
        /// </summary>
        void OnEnable()
        {
            if (quantumProperties == null || quantumProperties.Length == 0)
            {
                var prop = GetComponent<QuantumProperty>();
                if (prop != null)
                {
                    quantumProperties = new QuantumProperty[] { prop };
                }
                else
                {
                    Debug.LogError($"{gameObject.name}: No NativeQuantumProperty found on this object. Set properties to track");
                }
            }
        }

        /// <summary>
        /// Updates the probabilities if continuous tracking is enabled.
        /// </summary>
        void Update()
        {
            if (continuous)
            {
                _probabilities = GetBasisProbabilities();
            }
        }

        /// <summary>
        /// Calculates and returns the basis probabilities of the quantum properties.
        /// </summary>
        /// <returns>An array of basis probabilities.</returns>
        public QuantumProperty.BasisProbability[] GetBasisProbabilities()
        {
            _probabilities = QuantumProperty.Probabilities(quantumProperties);
            return _probabilities;
        }
    }
}

