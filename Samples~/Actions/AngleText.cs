﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRG.QuantumForge.Runtime;

namespace QRG.QuantumForge.Samples
{
    public class AngleText : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI text;
        public PhaseTracker phaseTracker;
        public int relativeIndex = 0;

        // Update is called once per frame
        void Update()
        {
            text.text = $"{phaseTracker.PhaseMatrix[0,relativeIndex] / (Mathf.PI):F2}π i";
        }
    }
}
