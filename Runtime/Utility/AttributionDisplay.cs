using UnityEngine;
using UnityEngine.SceneManagement;

public class AttributionDisplay : MonoBehaviour
{
    public Texture2D logo;
    public string attributionText = "Powered by Quantum Forge, © Quantum Realm Games, Inc. All rights reserved.";

    private void OnGUI()
    {
        // Display the logo at the top center of the screen
        if (logo != null)
        {
            float logoWidth = Screen.width * 0.2f;
            float logoHeight = logoWidth * ((float)logo.height / logo.width);
            GUI.DrawTexture(new Rect((Screen.width - logoWidth) / 2, 20, logoWidth, logoHeight), logo);
        }

        // Display the attribution text below the logo
        GUIStyle textStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.UpperCenter,
            fontSize = 16,
            wordWrap = true
        };

        float textWidth = Screen.width * 0.8f;
        GUI.Label(new Rect((Screen.width - textWidth) / 2, 100, textWidth, 100), attributionText, textStyle);
    }
}