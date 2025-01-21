using QRG.QuantumForge.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QRG.QuantumForge
{
    public class BasisColor : MonoBehaviour
    {
        public ColorProbability colorProbability;
        public int basisValue;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Image>().color = colorProbability.basisColors[basisValue];
        }
    }
}
