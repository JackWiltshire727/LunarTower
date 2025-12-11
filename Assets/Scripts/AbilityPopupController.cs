using UnityEngine;
using TMPro;
using System.Collections;

public class AbilityPopupController : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI abilityText;

    public float typingSpeed = 0.03f;
    private bool canClose = false;   
    private Coroutine typingCoroutine;

    void Start()
    {
        popupPanel.SetActive(false);
    }

    public void ShowAbility(string message)
    {
        Time.timeScale = 0f;
        popupPanel.SetActive(true);
        abilityText.text = "";
        canClose = false;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        foreach (char c in message)
        {
            abilityText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        canClose = true;
    }

    void Update()
    {
        if (popupPanel.activeSelf && canClose)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                ClosePopup();
            }
        }
    }

    public void ClosePopup()
    {
        Time.timeScale = 1f;
        popupPanel.SetActive(false);
    }
}
