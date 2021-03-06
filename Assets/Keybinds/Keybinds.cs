using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Keybinds : MonoBehaviour {

    [SerializeField] Text MoveLeftText = null;
    [SerializeField] Text MoveRightText = null;
    [SerializeField] Text JumpText = null;
    [SerializeField] Text DigAndPlaceText = null;

    [SerializeField] Text ErrorMSG = null;

    public enum Controls {
        MoveLeft,
        MoveRight,
        Jump,
        DigAndPlace
    }

    public static Dictionary<Controls, KeyCode> KeyControls = new Dictionary<Controls, KeyCode> {
        { Controls.MoveLeft, KeyCode.A },
        { Controls.MoveRight, KeyCode.D },
        { Controls.Jump, KeyCode.Space },
        { Controls.DigAndPlace, KeyCode.K }
    };

    string SelectedButtonName = "";

    void Start() {
        OnChangedKeybinds();
    }

    public void ButtonListener() {

        GameObject butt = EventSystem.current.currentSelectedGameObject;
        SelectedButtonName = butt.transform.parent.name;

        StartCoroutine(CheckInput());
    }

    IEnumerator CheckInput() {
        bool inputSent = false;

        while (!inputSent) {

            foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(kc)) {
                    inputSent = true;

                    ChangeKeybind(kc);

                    break;
                }
            }
            yield return null;
        }
    }

    void ChangeKeybind(KeyCode kc) {
        KeyValuePair<Controls, KeyCode> foundKV = KeyControls.FirstOrDefault(keyControl => keyControl.Value == kc);

        if (foundKV.Value == KeyCode.None) {
            if (kc != KeyCode.R) {
                foreach (KeyValuePair<Controls, KeyCode> kv in KeyControls) {
                    if (SelectedButtonName.Equals(kv.Key.ToString())) {
                        KeyControls[kv.Key] = kc;

                        OnChangedKeybinds();

                        ErrorMSG.gameObject.SetActive(false);
                        break;
                    }
                }
            }
            else {
                ErrorMSG.gameObject.SetActive(true);
                ErrorMSG.text = kc + " is already assigned to Reset!";
            }
        }
        else {
            ErrorMSG.gameObject.SetActive(true);
            ErrorMSG.text = kc + " is already assigned to " + foundKV.Key + "!";
        }
    }

    void OnChangedKeybinds() {
        PlayerMovement.MoveLeft = KeyControls[Controls.MoveLeft];
        MoveLeftText.text = KeyControls[Controls.MoveLeft].ToString();

        PlayerMovement.MoveRight = KeyControls[Controls.MoveRight];
        MoveRightText.text = KeyControls[Controls.MoveRight].ToString();

        PlayerMovement.Jump = KeyControls[Controls.Jump];
        JumpText.text = KeyControls[Controls.Jump].ToString();

        PlayerDig.DigAndPlace = KeyControls[Controls.DigAndPlace];
        DigAndPlaceText.text = KeyControls[Controls.DigAndPlace].ToString();

    }
}
