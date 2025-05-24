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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace QRG.QuantumForge.Runtime
{

    /// <summary>
    /// Defines the interface for quantum actions that can be applied to quantum properties.
    /// </summary>
    public interface IQuantumAction 
    {
        /// <summary>
        /// Gets or sets the predicates that determine the conditions for this action.
        /// </summary>
        public Predicate[] Predicates { get; set; }

        /// <summary>
        /// Gets or sets the quantum properties that this action targets.
        /// </summary>
        public QuantumProperty[] TargetProperties { get; set; }

        /// <summary>
        /// Applies the quantum action to the target quantum properties.
        /// </summary>
        public void apply();
    }

}
