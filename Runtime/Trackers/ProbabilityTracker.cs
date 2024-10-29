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
    public class ProbabilityTracker : MonoBehaviour
    {
        [SerializeField] private QuantumProperty[] quantumProperties;

        [SerializeField] private QuantumProperty.BasisProbability[] probabilities;
        public QuantumProperty.BasisProbability[] Probabilities => probabilities;

        [SerializeField] private bool continuous = true;

        // Start is called before the first frame update
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
                    Debug.LogError($"{gameObject.name}: No QuantumProperty found on this object. Set properties to track");
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (continuous)
            {
                probabilities = GetBasisProbabilities();
            }
        }

        public QuantumProperty.BasisProbability[] GetBasisProbabilities()
        {
            probabilities = QuantumProperty.Probabilities(quantumProperties);
            return probabilities;
        }
    }
}

