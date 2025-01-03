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
using System.Linq;
using QRG.QuantumForge.Runtime;
using UnityEngine;

namespace QRG.QuantumForge.Utility
{
    public class ScaleProbability : MonoBehaviour
    {
        [SerializeField] private ProbabilityTracker _probabilityTracker;

        [SerializeField] private BasisValue _scaleOnValue;

        [SerializeField] private bool _scaleX;
        [SerializeField] private bool _scaleY;
        [SerializeField] private bool _scaleZ;

        private Vector3 _initialScale;

        // Start is called before the first frame update
        void Start()
        {
            _initialScale = transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            var p = _probabilityTracker.Probabilities.Where(x => x.BasisValues[0] == _scaleOnValue);
            if (p.Any()) {
                var prob = p.First().Probability;
                transform.localScale = new Vector3(
                    _scaleX ? _initialScale.x * prob : _initialScale.x,
                    _scaleY ? _initialScale.y * prob : _initialScale.y,
                    _scaleZ ? _initialScale.z * prob : _initialScale.z
                );
            }

        }
    }
}
