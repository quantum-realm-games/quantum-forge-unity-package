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
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using QRG.QuantumForge.Core;
using Unity.Properties;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace QRG.QuantumForge.Runtime
{
    using QuantumForge = QuantumForge.Core.QuantumForge;

    /// <summary>
    /// Represents a condition or predicate for quantum operations.
    /// </summary>
    [Serializable]
    public class Predicate
    {
        /// <summary>
        /// The quantum property associated with this predicate.
        /// </summary>
        public QuantumProperty property = null;

        /// <summary>
        /// The basis value to compare against.
        /// </summary>
        [BasisValueDropdown]
        public BasisValue value;

        /// <summary>
        /// Indicates whether the predicate checks for equality or inequality.
        /// </summary>
        public bool is_equal;
    }

    /// <summary>
    /// Represents a quantum property and provides various quantum operations.
    /// This is the core of the Quantum Forge Toolkit, allowing you to add 
    /// quantum functionality to GameObjects and perform quantum operations on them.
    /// </summary>
    [Serializable]
    public class QuantumProperty : MonoBehaviour
    {
        private QuantumForge.NativeQuantumProperty _nativeNativeQuantumProperty;

        /// <summary>
        /// The basis associated with this quantum property.
        /// </summary>
        public Basis basis = null;

        /// <summary>
        /// The initial basis value for this quantum property.
        /// </summary>
        [SerializeField, BasisValueDropdown] private BasisValue Initial;

        /// <summary>
        /// The dimension of the basis associated with this quantum property.
        /// </summary>
        public int Dimension
        {
            get => basis.Dimension;
        }

        /// <summary>
        /// Initializes the quantum property and sets its initial state.
        /// </summary>
        void Awake()
        {
            try
            {
                if (basis == null)
                {
                    throw new Exception("Basis not set. Try setting basis in Editor. Reload/recompile sometimes corrupts this field.");
                }
                int initial = basis.values.IndexOf(Initial);
                if(initial == -1)
                {
                    initial = 0;
                    throw new Exception($"Initial value {Initial.Name} not found in basis, setting it to {basis.values[0].Name}. Try selecting initial value again in Editor. Reload/recompile sometimes corrupts this field.");
                }
                _nativeNativeQuantumProperty = new QuantumForge.NativeQuantumProperty(Dimension, initial);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning(gameObject.name + ": " + e.Message);
                _nativeNativeQuantumProperty = null;
            }

        }

        /// <summary>
        /// Creates a predicate that checks if the quantum property has a specific value.
        /// </summary>
        /// <param name="value">The basis value to check against.</param>
        /// <returns>A predicate representing the condition.</returns>
        public Predicate is_value(BasisValue value)
        {
            return new Predicate()
            {
                property = this,
                value = value,
                is_equal = true
            };
        }

        /// <summary>
        /// Creates a predicate that checks if the quantum property does not have a specific value.
        /// </summary>
        /// <param name="value">The basis value to check against.</param>
        /// <returns>A predicate representing the condition.</returns>
        public Predicate is_not_value(BasisValue value)
        {
            return new Predicate()
            {
                property = this,
                value = value,
                is_equal = false
            };
        }

        /// <summary>
        /// Converts an array of predicates to their native representation.
        /// </summary>
        /// <param name="predicates">The predicates to convert.</param>
        /// <returns>An array of native predicates.</returns>
        internal static QuantumForge.Predicate[] ConvertPredicates(Predicate[] predicates)
        {
            return Array.ConvertAll(predicates,
                p => new QuantumForge.Predicate(p.property._nativeNativeQuantumProperty,
                    p.property.basis.values.IndexOf(p.value), p.is_equal));
        }

        /// <summary>
        /// Applies a cycle operation to the quantum property.
        /// </summary>
        /// <param name="fraction">The fraction of the cycle to apply.</param>
        /// <param name="predicates">The conditions under which the operation is applied.</param>
        public void Cycle(float fraction, params Predicate[] predicates)
        {
            Cycle(this, fraction, predicates);
        }

        /// <summary>
        /// Applies a shift operation to the quantum property.
        /// </summary>
        /// <param name="fraction">The fraction of the shift to apply.</param>
        /// <param name="predicates">The conditions under which the operation is applied.</param>
        public void Shift(float fraction, params Predicate[] predicates)
        {
            Shift(this, fraction, predicates);
        }

        /// <summary>
        /// Applies a clock operation to the quantum property.
        /// </summary>
        /// <param name="fraction">The fraction of the clock cycle to apply.</param>
        /// <param name="predicates">The conditions under which the operation is applied.</param>
        public void Clock(float fraction, params Predicate[] predicates)
        {
            QuantumForge.Clock(_nativeNativeQuantumProperty, fraction, ConvertPredicates(predicates));
        }

        /// <summary>
        /// Applies a Hadamard operation to the quantum property.
        /// </summary>
        /// <param name="predicates">The conditions under which the operation is applied.</param>
        public void Hadamard(params Predicate[] predicates)
        {
            Hadamard(this, predicates);
        }

        /// <summary>
        /// Applies an inverse Hadamard operation to the quantum property.
        /// </summary>
        /// <param name="predicates">The conditions under which the operation is applied.</param>
        public void InverseHadamard(params Predicate[] predicates)
        {
            InverseHadamard(this, predicates);
        }

        /// <summary>
        /// Applies a phase rotation to the quantum property.
        /// </summary>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <param name="predicates">The conditions under which the operation is applied.</param>
        public static void PhaseRotate(float angle, params Predicate[] predicates)
        {
            QuantumForge.PhaseRotate(angle, ConvertPredicates(predicates));
        }

        /// <summary>
        /// Swaps the states of two quantum properties.
        /// </summary>
        /// <param name="prop1">The first quantum property.</param>
        /// <param name="prop2">The second quantum property.</param>
        /// <param name="predicates">The conditions under which the operation is applied.</param>
        public static void Swap(QuantumProperty prop1, QuantumProperty prop2, params Predicate[] predicates)
        {
            QuantumForge.Swap(prop1._nativeNativeQuantumProperty, prop2._nativeNativeQuantumProperty, ConvertPredicates(predicates));
        }

        /// <summary>
        /// Performs an entangling operation between two quantum properties.
        /// </summary>
        /// <param name="prop1">The first quantum property.</param>
        /// <param name="prop2">The second quantum property.</param>
        public static void NCycle(QuantumProperty prop1, QuantumProperty prop2)
        {
            QuantumForge.NCycle(prop1._nativeNativeQuantumProperty, prop2._nativeNativeQuantumProperty);
        }


        [Serializable]
        public struct BasisProbability
        {
            public float Probability;
            public BasisValue[] BasisValues;
            [SerializeField] private string _basisValues; // Editor conveinence

            public BasisProbability(float probability, BasisValue[] basisValues)
            {
                Probability = probability;
                BasisValues = basisValues;
                _basisValues = string.Join(",", basisValues.Select(x => x.Name));
            }

            public override string ToString()
            {
                return $"{Probability.ToString("0.00")} : {string.Join(",", BasisValues.Select(x => x.Name))}";
            }
        }

        /// <summary>
        /// Calculates the probabilities of basis states for the specified quantum properties.
        /// </summary>
        /// <param name="properties">The quantum properties to calculate probabilities for.</param>
        /// <returns>An array of basis probabilities.</returns>
        public static BasisProbability[] Probabilities(params QuantumProperty[] properties)
        {
            if (properties == null || properties.Length == 0)
            {
                Debug.LogWarning("No properties provided to calculate probabilities");
                return new BasisProbability[0];
            }
            var props = Array.ConvertAll(properties, p => p._nativeNativeQuantumProperty);
            var probs = QuantumForge.Probabilities(props);
            var numValues = properties.Length;
            var result = new BasisProbability[probs.Length];
            for (int i = 0; i < probs.Length; i++)
            {
                var values = new BasisValue[properties.Length];
                for (int j = 0; j < properties.Length; ++j)
                {
                    values[j] = properties[j].basis.values[probs[i].QuditValues[j]];
                }

                result[i] = new BasisProbability(probs[i].Probability, values);
            }

            return result;
        }

        /// <summary>
        /// Calculates the reduced density matrix for the specified quantum properties.
        /// </summary>
        /// <param name="properties">The quantum properties to calculate the matrix for.</param>
        /// <returns>The reduced density matrix as a 2D complex array.</returns>
        public static Complex[,] ReducedDensityMatrix(params QuantumProperty[] properties)
        {
            var props = Array.ConvertAll(properties, p => p._nativeNativeQuantumProperty);
            return QuantumForge.ReducedDensityMatrix(props);
        }

        /// <summary>
        /// Measures the quantum properties and returns the resulting basis indices.
        /// </summary>
        /// <param name="properties">The quantum properties to measure.</param>
        /// <returns>An array of basis indices representing the measurement results.</returns>
        public static int[] Measure(params QuantumProperty[] properties)
        {
            var props = Array.ConvertAll(properties, p => p._nativeNativeQuantumProperty);
            return QuantumForge.Measure(props);
        }
        public static int Measure(params Predicate[] predicates)
        {
            return QuantumForge.Measure(ConvertPredicates(predicates));
        }

    }
}