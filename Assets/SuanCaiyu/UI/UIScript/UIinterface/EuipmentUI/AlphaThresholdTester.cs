using UnityEngine;
using UnityEngine.UI;

public class AlphaThresholdTester : MonoBehaviour
{
    public Image targetImage;
    [Range(0, 1)] public float testThreshold = 0.1f;

    void Update()
    {
        if (targetImage != null)
        {
            targetImage.alphaHitTestMinimumThreshold = testThreshold;
        }
    }

    void OnGUI()
    {
        GUILayout.Label($"µ±«∞Alpha„–÷µ: {testThreshold}");
        testThreshold = GUILayout.HorizontalSlider(testThreshold, 0f, 1f);
    }
}