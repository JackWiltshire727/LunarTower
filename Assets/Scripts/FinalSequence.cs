using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinalSequence : MonoBehaviour
{
    public TextMeshProUGUI finalText;     // For the 3 narrative lines
    public Image fadePanel;               // Fullscreen black panel
    public TextMeshProUGUI thanksText;    // "Thanks for playing"

    private string[] lines = new string[]
    {
        "Your climb ends in magnificent moonlight.",
        "You have conquered what no wizard before you could.",
        "You will be known across the lands as one of the greatest wizards."
    };

    public float lineFadeInTime = 1.5f;       // Fade time per sentence
    public float lineReadDelay = 2.5f;        // Time each sentence stays visible
    public float fadeToBlackTime = 2f;        // Time to fade screen to black
    public float thanksFadeTime = 2f;         // Fade time for "Thanks for playing"
    public float thanksDisplayTime = 3f;      // How long it stays visible afterward

    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        finalText.text = "";

        // --- Step 1: Show each of the 3 sentences ---
        foreach (string line in lines)
        {
            finalText.text = line;

            // Start invisible
            finalText.color = new Color(finalText.color.r, finalText.color.g, finalText.color.b, 0);

            float t = 0f;
            while (t < lineFadeInTime)
            {
                t += Time.deltaTime;
                float a = Mathf.Lerp(0, 1, t / lineFadeInTime);
                finalText.color = new Color(finalText.color.r, finalText.color.g, finalText.color.b, a);
                yield return null;
            }

            // Wait long enough for the player to read it
            yield return new WaitForSeconds(lineReadDelay);
        }

        // --- Step 2: Fade the entire screen to black ---
        float f = 0f;
        while (f < fadeToBlackTime)
        {
            f += Time.deltaTime;
            float a = Mathf.Lerp(0, 1, f / fadeToBlackTime);
            fadePanel.color = new Color(0, 0, 0, a);
            yield return null;
        }

        // Hide narrative text (it's behind the black now anyway)
        finalText.text = "";

        // --- Step 3: Fade in "Thanks for playing" ---
        thanksText.color = new Color(thanksText.color.r, thanksText.color.g, thanksText.color.b, 0);

        float t2 = 0f;
        while (t2 < thanksFadeTime)
        {
            t2 += Time.deltaTime;
            float a = Mathf.Lerp(0, 1, t2 / thanksFadeTime);
            thanksText.color = new Color(thanksText.color.r, thanksText.color.g, thanksText.color.b, a);
            yield return null;
        }

        // Let it stay on screen
        yield return new WaitForSeconds(thanksDisplayTime);

        // (Optional: Quit game or load main menu)
        // SceneManager.LoadScene("MainMenu");
        // Application.Quit();
    }
}
