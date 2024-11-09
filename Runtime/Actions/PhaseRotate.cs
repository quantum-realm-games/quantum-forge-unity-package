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

    [Serializable]
    public class PhaseRotate : MonoBehaviour, IQuantumAction
    {
        [SerializeField] private QuantumProperty[] _targetProperties;
        public QuantumProperty[] TargetProperties => _targetProperties;

        [SerializeField] private BasisValues _basisValues = null;

        [SerializeField, Dropdown("_basisValues.values")]
        private string _value;

        [SerializeField, Range(0, 2*Mathf.PI)] private float _radians = 1.0f;
        public float Radians => _radians;

        public void apply()
        {
            apply(TargetProperties);
        }

        public void apply(params QuantumProperty[] targetProperties)
        {
            foreach (var quantumProperty in targetProperties)
            {
                if(quantumProperty.BasisValues != _basisValues)
                {
                    Debug.LogWarning($"Basis values of {quantumProperty.gameObject.name} do not match the basis values of the phase rotate action. Skipping.");
                    continue;
                }
                Debug.Log($"Applying phase rotation to {quantumProperty.gameObject.name}");
                quantumProperty.PhaseRotate(_radians,_value);
            }
        }

        public void SetPhaseRadians(float radians)
        {
            _radians = radians;
        }
    }

}
