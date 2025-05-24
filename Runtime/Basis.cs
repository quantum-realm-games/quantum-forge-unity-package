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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace QRG.QuantumForge.Runtime
{

    /// <summary>
    /// Represents a value in a quantum basis.
    /// </summary>
    [Serializable]
    public class BasisValue : IEquatable<BasisValue>
    {
        /// <summary>
        /// The name of the basis value.
        /// </summary>
        public string Name;

        /// <summary>
        /// Determines whether the specified <see cref="BasisValue"/> is equal to the current instance.
        /// </summary>
        /// <param name="other">The other <see cref="BasisValue"/> to compare.</param>
        /// <returns><c>true</c> if the specified <see cref="BasisValue"/> is equal to the current instance; otherwise, <c>false</c>.</returns>
        public bool Equals(BasisValue other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BasisValue)obj);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }


    // Suggestion, keep Basis list small.
    /// <summary>
    /// Represents a quantum basis, which is a collection of basis values.
    /// </summary>
    [CreateAssetMenu(fileName = "Basis", menuName = "Quantum/Basis", order = 1)]
    public class Basis : ScriptableObject
    {
        /// <summary>
        /// The list of basis values in this quantum basis.
        /// </summary>
        public List<BasisValue> values;

        /// <summary>
        /// Gets the dimension of the quantum basis, which is the number of basis values.
        /// </summary>
        public int Dimension
        {
            get { return values.Count; }
        }

    }

}

