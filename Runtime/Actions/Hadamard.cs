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
using System.Diagnostics;
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    [Serializable]
    public class Hadamard : MonoBehaviour, IQuantumAction
    {
        [field: SerializeField] public QuantumProperty.Predicate[] Predicates { get; set; }
        [field: SerializeField] public QuantumProperty[] TargetProperties { get; set; }

        public void apply()
        {
            foreach (var prop in TargetProperties)
            {
                UnityEngine.Debug.Log($"Applying Hadamard to {prop} with {Predicates.Length} predicates");
                prop.Hadamard(Predicates);
            }
        }
    }
}
