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
using UnityEngine.Events;
using QRG.QuantumForge.Runtime;


namespace QRG.QuantumForge.Runtime
{
    [System.Serializable]
    public class QuantumPropertyEvent : UnityEvent<QuantumProperty> { };

    public class QuantumPropertyTrigger : MonoBehaviour
    {
        [SerializeField] private QuantumPropertyEvent onTriggerEnter, onTriggerExit;

        void OnTriggerEnter2D(Collider2D otherCollider)
        {
            var q = otherCollider.gameObject.GetComponent<QuantumProperty>();
            if (q != null)
            {
                onTriggerEnter.Invoke(q);
            }
        }

        void OnTriggerEnter(Collider otherCollider)
        {
            var q = otherCollider.gameObject.GetComponent<QuantumProperty>();
            if (q != null)
            {
                onTriggerEnter.Invoke(q);
            }
        }
    }
}