using System.Collections.Generic;
using UnityEngine;

public class TutorialTextCollider : MonoBehaviour {
    [SerializeField] TutorialNames TutorialName = TutorialNames.Move;

    [SerializeField] TutorialStyle TutStyle = TutorialStyle.KeyCode;
    public KeyCode KeyCode;
    public List<KeyCode> KeyCodes;
    public bool Last = false;

    enum TutorialNames {
        Move,
        Jump,
        Dig,
        Place
    }

    enum TutorialStyle {
        Inventory,
        KeyCode,
        KeyCodes,
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {

            Tutorial tutorial = GetComponentInParent<Tutorial>();

            switch (TutStyle) {
                case TutorialStyle.Inventory:
                    tutorial.StartTutorial(TutorialName.ToString());
                    break;

                case TutorialStyle.KeyCode:
                    tutorial.StartTutorial(TutorialName.ToString(), KeyCode);
                    break;

                case TutorialStyle.KeyCodes:
                    tutorial.StartTutorial(TutorialName.ToString(), KeyCodes);
                    break;
            }

            tutorial.Last = Last;
        }
    }
}
