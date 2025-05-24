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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using QRG.QuantumForge.Runtime;
using Unity.VisualScripting;

namespace QRG.QuantumForge.Runtime
{
    /// <summary>
    /// Tracks mutual information to measure entanglement between specified quantum properties.
    /// </summary>
    public class EntanglementTracker : MonoBehaviour
    {
        /// <summary>
        /// Quantum properties to track entanglement for.
        /// </summary>
        [Tooltip("Quantum properties to track entanglement for.")]
        [SerializeField] private QuantumProperty[] quantumProperties;

        /// <summary>
        /// Array representing the mutual information between quantum properties.
        /// </summary>
        [Tooltip("Array representing the mutual information between quantum properties.")]
        [SerializeField] private float[] mutualInformation;

        public float[] LastUpdatedMutualInformation => mutualInformation;

        /// <summary>
        /// Indicates whether the mutual information should be updated continuously.
        /// </summary>
        [Tooltip("Indicates whether the mutual information should be updated continuously.")]
        [SerializeField] private bool continuous = true;

        /// <summary>
        /// Updates the mutual information if continuous tracking is enabled.
        /// </summary>
        void Update()
        {
            if (continuous)
            {
                UpdateMutualInformation();
            }
        }

        public float[] UpdateMutualInformation()
        {
            if (quantumProperties == null || quantumProperties.Length < 2)
            {
                Debug.LogError($"{gameObject.name}: Set at least two properties to track");
                return null;
            }

            mutualInformation = QuantumProperty.MutualInformation(quantumProperties);
            return mutualInformation;
        }
    }
}

