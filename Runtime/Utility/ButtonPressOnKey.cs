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
using UnityEngine.UI;

namespace QRG.QuantumForge.Utility
{
    public class ButtonPressOnKey : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private KeyCode key;

        [SerializeField] private TMPro.TextMeshProUGUI text;

        void Start()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }

            if (_button == null)
            {
                Debug.LogError($"{gameObject.name}: ButtonPressOnKey needs a button");
            }

            if (text != null)
            {
                text.text = key.ToString();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                _button.onClick.Invoke();
                FadeToColor(_button.colors.pressedColor);
            }
            else if (Input.GetKeyUp(key))
            {
                FadeToColor(_button.colors.normalColor);
            }
        }

        void FadeToColor(Color color)
        {
            var graphic = _button.targetGraphic;
            graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
        }
    }
}
