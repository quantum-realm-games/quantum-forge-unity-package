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
    public class MeasurePredicates : MonoBehaviour, IQuantumAction
    {
        public UnityEvent OnMeasure;
        public QuantumPropertyEvent OnMeasureQuantumProperty;

        [field: SerializeField] public Predicate[] Predicates { get; set; }

        public QuantumProperty[] TargetProperties { get; set; }// not shown in inspector

        [field: SerializeField] public int LastResult { get; private set; }

        public void apply()
        {
            if (TargetProperties.Length == 0) return;
            LastResult = QuantumProperty.Measure(Predicates);
            OnMeasure.Invoke();
        }

    }

}
