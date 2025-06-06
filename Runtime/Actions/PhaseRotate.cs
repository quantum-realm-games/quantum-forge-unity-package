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
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    /// <summary>
    /// Represents a quantum action that applies a phase rotation to quantum properties.
    /// </summary>
    [Serializable]
    public class PhaseRotate : MonoBehaviour, IQuantumAction
    {
        /// <summary>
        /// Gets or sets the predicates that determine the conditions for this action.
        /// </summary>
        [Tooltip("Predicates that determine the conditions for this action.")]
        [SerializeField]
        Predicate[] _predicates;
        public Predicate[] Predicates
        {
            get { return _predicates; }
            set { _predicates = value; }
        }

        /// <summary>
        /// Gets or sets the quantum properties that this action targets.
        /// </summary>
        [Tooltip("Quantum properties that this action targets.")]
        public QuantumProperty[] TargetProperties
        {
            get => GetProperties(Predicates);
            set { Debug.LogError($"Attempting to set TargetProperties for PhaseRotate on {gameObject.name}. Must set Predicates instead."); }
        }

        /// <summary>
        /// Gets or sets the angle of rotation in radians.
        /// </summary>
        [Tooltip("The angle of rotation in radians.")]
        [field: SerializeField, Range(0, 2 * Mathf.PI)] public float Radians { get; set; }

        /// <summary>
        /// Retrieves the quantum properties associated with the given predicates.
        /// </summary>
        /// <param name="predicates">The predicates to retrieve properties for.</param>
        /// <returns>An array of quantum properties.</returns>
        private static QuantumProperty[] GetProperties(params Predicate[] predicates)
        {
            var properties = new List<QuantumProperty>();
            foreach (var predicate in predicates)
            {
                properties.Add(predicate.property);
            }
            return properties.ToArray();
        }

        /// <summary>
        /// Applies the phase rotation to the target quantum properties.
        /// </summary>
        public void apply()
        {
            Debug.Log($"Applying phase rotation {Radians} with {Predicates.Length} predicates.");
            QuantumProperty.PhaseRotate(Radians, Predicates);
        }

    }

}
