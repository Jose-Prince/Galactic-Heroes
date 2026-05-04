using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerminalAnimator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI terminalText;
    [SerializeField] float charDelay = 0.03f;
    [SerializeField] float lineDelay = 0.6f;

    string[] lines =
    {
        "> BOOTING FLIGHT SYSTEMS",
        "> CALIBRATING THRUST VECTORS",
        "> SCANNING CHECKPOINT MATRIX",
        "> SYNCHRONIZING NAVIGATION CORE",
        "> ESTABLISHING ORBITAL LINK",
        "> RACE CORRIDOR LOCKED",
        "> PILOT AUTHORIZED",
        "> READY"
    };
    
    void Start()
    {
        StartCoroutine(TypeRoutine());
    }

    IEnumerator TypeRoutine()
    {
        terminalText.text = "";

        foreach (string line in lines)
        {
            foreach (char c in line)
            {
                terminalText.text += c;
                yield return new WaitForSeconds(charDelay);
            }

            terminalText.text += "\n";
            yield return new WaitForSeconds(lineDelay);
        }
        StartCoroutine(BlinkCursor());
    }

    IEnumerator BlinkCursor()
    {
        while(true)
        {
            terminalText.text += "_";
            yield return new WaitForSeconds(0.4f);

            terminalText.text = 
                terminalText.text.Substring(0, terminalText.text.Length - 1);

            yield return new WaitForSeconds(0.4f);
        }
    }
}
