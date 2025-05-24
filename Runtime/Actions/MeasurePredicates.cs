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
using UnityEngine.Events;

namespace QRG.QuantumForge.Runtime
{

    [Serializable]
    public class MeasurePredicateEvent : UnityEvent<bool> { };

    /// <summary>
    /// Represents a quantum action that measures predicates and triggers events.
    /// </summary>
    [Serializable]
    public class MeasurePredicates : MonoBehaviour, IQuantumAction
    {
        /// <summary>
        /// Event triggered when a measurement is performed.
        /// </summary>
        [Tooltip("Event triggered when a measurement is performed.")]
        public UnityEvent OnMeasure;

        /// <summary>
        /// Event triggered with a QuantumProperty when a measurement is performed.
        /// </summary>
        [Tooltip("Event triggered with a bool when a measurement is performed. Triggers with true if the predicates are satisfied, false otherwise.")]
        public MeasurePredicateEvent OnMeasurePredicates;
        /// <summary>

        /// Gets or sets the predicates that determine the conditions for this action.
        /// </summary>
        [Tooltip("Predicates that determine the conditions for this action.")]
        [field: SerializeField] public Predicate[] Predicates { get; set; }

        /// <summary>
        /// Gets or sets the quantum properties that this action targets.
        /// </summary>
        public QuantumProperty[] TargetProperties { get; set; } // not shown in inspector

        /// <summary>
        /// Gets the last result of the measurement.
        /// </summary>
        [Tooltip("The last result of the measurement.")]
        [field: SerializeField] public int LastResult { get; private set; }

        /// <summary>
        /// Applies the measurement action to the target quantum properties.
        /// </summary>
        public void apply()
        {
            if (Predicates.Length == 0) return;
            LastResult = QuantumProperty.Measure(Predicates);
            OnMeasure.Invoke();
        }

    }

}
