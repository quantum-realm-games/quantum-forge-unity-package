using TMPro;
using UnityEngine;

namespace QRG.QuantumForge
{
    public class CHSHReferee : MonoBehaviour
    {
        public int X = -1;
        public int Y = -1;

        public void Reset()
        {
            X = Random.Range(0, 2);
            Y = Random.Range(0, 2);
        }

        public bool EvaluateWin(int a, int b)
        {
            if (X == 1 && Y == 1)
            {
                if (a != b) return true;
            }
            else
            {
                if (a == b) return true;
            }

            return false;
        }
    }
}
