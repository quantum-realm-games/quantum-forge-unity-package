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
using System.Numerics;
using System.Runtime.InteropServices;
using QRG.QuantumForge.Core;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace QRG.QuantumForge.Runtime
{
    using QuantumForge = QuantumForge.Core.QuantumForge;

    [Serializable]
    public class QuantumProperty : MonoBehaviour
    {
        [SerializeField] private Basis _basis = null;
        private QuantumForge.QuantumProperty _nativeQuantumProperty;

        [SerializeField, Dropdown("_basis.values")]
        private string Initial;

        public int Dimension
        {
            get => _basis.Dimension;
        }

        public Basis Basis
        {
            get => _basis;
        }

        void Awake()
        {
            try
            {
                if (_basis == null)
                {
                    throw new Exception("Basis not set. Try setting basis in Editor. Reload/recompile sometimes corrupts this field.");
                }
                int initial = _basis.values.IndexOf(Initial);
                if(initial == -1)
                {
                    initial = 0;
                    throw new Exception($"Initial value {initial} not found in basis, setting it to {_basis.values[0]}. Try selecting initial value again in Editor. Reload/recompile sometimes corrupts this field.");
                }
                _nativeQuantumProperty = new QuantumForge.QuantumProperty(Dimension, initial);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning(gameObject.name + ": " + e.Message);
                _nativeQuantumProperty = null;
            }

        }

        public struct Predicate
        {
            public QuantumProperty property;
            public string value;
            public bool is_equal;
        }

        public Predicate is_value(string value)
        {
            return new Predicate()
            {
                property = this,
                value = value,
                is_equal = true
            };
        }

        public Predicate is_value(int value)
        {
            return is_value(_basis.values[value]);
        }

        public Predicate is_not_value(string value)
        {
            return new Predicate()
            {
                property = this,
                value = value,
                is_equal = false
            };
        }

        public Predicate is_not_value(int value)
        {
            return is_value(_basis.values[value]);
        }

        internal static QuantumForge.Predicate[] ConvertPredicates(Predicate[] predicates)
        {
            return Array.ConvertAll(predicates,
                p => new QuantumForge.Predicate(p.property._nativeQuantumProperty,
                    p.property._basis.values.IndexOf(p.value), p.is_equal));
        }

        public static void Cycle(QuantumProperty prop, params Predicate[] predicates)
        {
            QuantumForge.Cycle(prop._nativeQuantumProperty, ConvertPredicates(predicates));
        }

        public void Cycle(params Predicate[] predicates)
        {
            Cycle(this, predicates);
        }

        public static void ReverseCycle(QuantumProperty prop, params Predicate[] predicates)
        {
            var dimension = prop.Dimension;
            for (int i = 0; i < dimension; ++i)
            {
                QuantumForge.Cycle(prop._nativeQuantumProperty, ConvertPredicates(predicates));
            }
        }

        public void ReverseCycle(params Predicate[] predicates)
        {
            ReverseCycle(this, predicates);
        }

        public static void Hadamard(QuantumProperty prop, params Predicate[] predicates)
        {
            QuantumForge.Hadamard(prop._nativeQuantumProperty, ConvertPredicates(predicates));
        }

        public void Hadamard(params Predicate[] predicates)
        {
            Hadamard(this, predicates);
        }

        public static void PhaseRotate(float angle, params Predicate[] predicates)
        {
            QuantumForge.PhaseRotate(angle, ConvertPredicates(predicates));
        }

        public void PhaseRotate(float angle, params string[] values)
        {
            var predicates = new Predicate[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                predicates[i] = this.is_value(values[i]);
            }

            PhaseRotate(angle, predicates);
        }

        public static void PhaseFlip(params Predicate[] predicates)
        {
            QuantumForge.PhaseFlip(ConvertPredicates(predicates));
        }

        public static void PhaseAll(params QuantumProperty[] properties)
        {
            var props = Array.ConvertAll(properties, p => p._nativeQuantumProperty);
            QuantumForge.PhaseZ(props);
        }

        /// <summary>
        /// The full qudit Z gate.
        /// Applies a phase rotation to all basis values, based on the value.
        /// Let w = exp(2*pi*i/Dimension)
        /// For basis value v, the phase rotation is w^v
        /// </summary>
        public void PhaseAll()
        {
            QuantumForge.PhaseZ(_nativeQuantumProperty);
        }

        public void PhaseFlip(params string[] values)
        {
            var predicates = new Predicate[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                predicates[i] = this.is_value(values[i]);
            }

            PhaseFlip(predicates);
        }

        public static void Swap(QuantumProperty prop1, QuantumProperty prop2, params Predicate[] predicates)
        {
            QuantumForge.Swap(prop1._nativeQuantumProperty, prop2._nativeQuantumProperty, ConvertPredicates(predicates));
        }

        public static void ISwap(QuantumProperty prop1, QuantumProperty prop2)
        {
            QuantumForge.ISwap(prop1._nativeQuantumProperty, prop2._nativeQuantumProperty);
        }

        public static void FractionalISwap(QuantumProperty prop1, QuantumProperty prop2, float fraction)
        {
            QuantumForge.FractionalISwap(prop1._nativeQuantumProperty, prop2._nativeQuantumProperty, fraction);
        }

        /// <summary>
        /// Entangling operation. Performs a number of predicated (controlled) cycles on prop2 based on the value of prop1.
        /// Ex. Prop1 is in superposition of 0, 1, and 2 : |prop1> = 1/sqrt(3) * (|0> + |1> + |2>)
        ///     Prop2 starts in state 0: |prop2> = |0>
        ///     Result: |prop1,prop2> = 1/sqrt(3) * (|0,0> + |1,1> + |2,2>)
        /// Note: if prop2 starts in a different state, the result will be a different entanglement structure.
        /// Ex. Prop2 starts in state 1: |prop2> = |1>
        ///     Result: |prop1,prop2> = 1/sqrt(3) * (|0,1> + |1,2> + |2,0>)
        /// </summary>
        /// <param name="prop1"></param>
        /// <param name="prop2"></param>
        public static void NCycle(QuantumProperty prop1, QuantumProperty prop2)
        {
            QuantumForge.NCycle(prop1._nativeQuantumProperty, prop2._nativeQuantumProperty);
        }


        [Serializable]
        public struct BasisProbability
        {
            public float Probability;
            public string[] BasisValues;
            [SerializeField] private string _basisValues; // Editor conveinence

            public BasisProbability(float probability, string[] basisValues)
            {
                Probability = probability;
                BasisValues = basisValues;
                _basisValues = string.Join(",", basisValues);
            }

            public override string ToString()
            {
                return $"{Probability.ToString("0.00")} : {string.Join(",", BasisValues)}";
            }
        }

        public static BasisProbability[] Probabilities(params QuantumProperty[] properties)
        {
            if (properties == null || properties.Length == 0)
            {
                Debug.LogWarning("No properties provided to calculate probabilities");
                return new BasisProbability[0];
            }
            var props = Array.ConvertAll(properties, p => p._nativeQuantumProperty);
            var probs = QuantumForge.Probabilities(props);
            var numValues = properties.Length;
            var result = new BasisProbability[probs.Length];
            for (int i = 0; i < probs.Length; i++)
            {
                var values = new string[properties.Length];
                for (int j = 0; j < properties.Length; ++j)
                {
                    values[j] = properties[j]._basis.values[probs[i].QuditValues[j]];
                }

                result[i] = new BasisProbability(probs[i].Probability, values);
            }

            return result;
        }

        public static Complex[,] ReducedDensityMatrix(params QuantumProperty[] properties)
        {
            var props = Array.ConvertAll(properties, p => p._nativeQuantumProperty);
            return QuantumForge.ReducedDensityMatrix(props);
        }

        public static float[] MutualInformation(params QuantumProperty[] properties)
        {
            var props = Array.ConvertAll(properties, p => p._nativeQuantumProperty);
            return QuantumForge.MutualInformation(props);
        }

        public static float[,] CorrelationMatrix(params QuantumProperty[] properties)
        {
            var props = Array.ConvertAll(properties, p => p._nativeQuantumProperty);
            return QuantumForge.CorrelationMatrix(props);
        }

        public static int[] Measure(params QuantumProperty[] properties)
        {
            var props = Array.ConvertAll(properties, p => p._nativeQuantumProperty);
            return QuantumForge.Measure(props);
        }

    }
}