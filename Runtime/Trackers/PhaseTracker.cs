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
using System.Numerics;
using UnityEngine;
using QRG.QuantumForge.Runtime;
using Unity.VisualScripting;

namespace QRG.QuantumForge.Runtime
{
    public class PhaseTracker : MonoBehaviour
    {
        [SerializeField] private QuantumProperty[] quantumProperties;

        [SerializeField] private float[,] phaseMatrix;
        public float[,] PhaseMatrix => phaseMatrix;

        [SerializeField] private bool continuous = true;
        [SerializeField] private bool radians = true;

        [SerializeField, TextArea(5,20)] private string matrixData = "";

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

            int size = 1;
            foreach (var prop in quantumProperties)
            {
                size *= prop.Dimension;
            }
            phaseMatrix = new float[size, size];
        }

        // Update is called once per frame
        void Update()
        {
            if (continuous)
            {
                UpdatePhaseMatrix();
            }
        }

        public float[,] UpdatePhaseMatrix()
        {
            var rdm = QuantumProperty.ReducedDensityMatrix(quantumProperties);
            for (int i = 0; i < rdm.GetLength(0); ++i)
            {
                for (int j = 0; j < rdm.GetLength(1); ++j)
                {
                    phaseMatrix[i,j] = (float)rdm[i, j].Phase;
                    if(!radians)
                    {
                        phaseMatrix[i, j] *= 180 / Mathf.PI;
                    }
                }
            }
            SetMatrixData();
            return phaseMatrix;
        }

        private void SetMatrixData()
        {
            matrixData = "";
            for(int i = 0; i < phaseMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < phaseMatrix.GetLength(1); j++)
                {
                    matrixData += phaseMatrix[i, j].ToString("0.00") + " ";
                }
                matrixData += "\n";
            }
        }
    }
}

