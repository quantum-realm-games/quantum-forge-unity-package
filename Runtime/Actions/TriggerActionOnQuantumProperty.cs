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
using UnityEngine.Events;

namespace QRG.QuantumForge.Runtime
{

    /// <summary>
    /// Represents a component that triggers a quantum action when a QuantumProperty is detected.
    /// </summary>
    public class TriggerActionOnQuantumProperty : MonoBehaviour
    {
        /// <summary>
        /// Triggers the quantum action on the specified QuantumProperty.
        /// </summary>
        /// <param name="quantumProperty">The QuantumProperty to act upon.</param>
        void TriggerAction(QuantumProperty quantumProperty)
        {
            if (quantumProperty == null)
            {
                return;
            }
            var action = GetComponent<IQuantumAction>();
            action.TargetProperties = new QuantumProperty[] { quantumProperty };
            action.apply();
        }

        /// <summary>
        /// Called when another collider enters this collider (2D).
        /// </summary>
        /// <param name="otherCollider">The collider that entered.</param>
        void OnTriggerEnter2D(Collider2D otherCollider)
        {
            var q = otherCollider.gameObject.GetComponent<QuantumProperty>();
            TriggerAction(q);
        }

        /// <summary>
        /// Called when another collider enters this collider (3D).
        /// </summary>
        /// <param name="otherCollider">The collider that entered.</param>
        void OnTriggerEnter(Collider otherCollider)
        {
            Debug.Log("Trigger enter");
            var q = otherCollider.gameObject.GetComponent<QuantumProperty>();
            TriggerAction(q);
        }
    }
}
