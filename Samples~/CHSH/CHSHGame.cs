using UnityEngine;

namespace QRG.QuantumForge
{
    [RequireComponent(typeof(InitializeCHSH))]
    public class CHSHGame : MonoBehaviour
    {
        public bool isQuantum;
        public int rounds = 0;
        public int wins = 0;

        public CHSHReferee referee;
        public CHSHPlayer playerA;
        public CHSHPlayer playerB;

        void Start()
        {
            Initialize();
        }

        public void Go(int runs = 1)
        {
            for (int i = 0; i < runs; i++)
            {
                Initialize();
                referee.Reset();

                int a = playerA.Play(referee.X);
                int b = playerB.Play(referee.Y);
                if (referee.EvaluateWin(a, b)) wins++;
                rounds++;
            }
        }

        public void Initialize()
        {
            var initializer = GetComponent<InitializeCHSH>();
            if (!isQuantum) initializer.InitializeClassical();
            else initializer.InitializeEntangled();
        }

        public void Reset()
        {
            rounds = 0;
            wins = 0;
            playerA.Reset();
            playerB.Reset();
            Initialize();
        }
    }
}
