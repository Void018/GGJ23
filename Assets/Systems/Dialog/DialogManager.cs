using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour {
    public static DialogManager instance;
    [HideInInspector]
    public UnityEvent<int> endedDialog = default;

    public GameObject dialogPanel;
    private TMP_Text textComponent;
    public float textSpeed = 0.05f;
    [HideInInspector] public bool onDialog;

    private int index;
    private string[] lines;
    private int signIndex = -1;

    private void Awake() {
        instance = this;
    }

    void Start() {
        textComponent = dialogPanel.GetComponentInChildren<TMP_Text>();
    }

    void Update() {
        if (!dialogPanel.activeSelf) return;

        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == lines[index]) {
                NextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialog(string[] lines, int signIndex = -1) {
        // if there a dialog right now, cancel dialog request
        if (dialogPanel.activeSelf) {
            return;
        } else {
            dialogPanel.SetActive(true);
        }
        this.lines = lines;
        index = 0;
        onDialog = true;
        if (index > -1) this.signIndex = signIndex;
        StartCoroutine(TypeLine());
    }

    public void EndDialog() {
        onDialog = false;
        lines = null;
        dialogPanel.SetActive(false);
        if (signIndex > -1) {
            endedDialog?.Invoke(signIndex);
        }
    }

    IEnumerator TypeLine() {
        textComponent.text = "";
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            SoundManager.instance.Play(Sounds.Write);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            StartCoroutine(TypeLine());
        } else {
            EndDialog();
        }
    }
}
