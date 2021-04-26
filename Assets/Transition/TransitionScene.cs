using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScene : MonoBehaviour
{
    public static string PickUpLineText;
    public static string RejectionLineText;

    [SerializeField] TypeWriterEffect PickUpLine = null;
    [SerializeField] TypeWriterEffect RejectionLine = null;

    [SerializeField] GameObject Player = null;
    [SerializeField] Animator PlayerAnimator = null;

    [SerializeField] GameObject GyuStyfe = null;
    [SerializeField] Animator GyuStyfeAnimator = null;

    [SerializeField] Animator ContinueText = null;
    
    public ContinueScript ContinueTextScript = null;
    public string animationToPlay = "fade_in";

    readonly float Delay = 0.1f;

    bool StartMove = true;
    bool EndMove = false;

    public Vector3 PlayerStartPosition;
    public Vector3 PlayerEndPosition;

    public Vector3 GyuStyfeStartPosition;
    public Vector3 GyuStyfeEndPosition;

    public float MoveSpeed = 5f;

    void Awake() {
        PickUpLine.fullText = PickUpLineText;
        RejectionLine.fullText = RejectionLineText; 
    }

    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(CheckIsDone());
    }

    void FixedUpdate() {
        if (StartMove) {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerStartPosition, MoveSpeed * Time.fixedDeltaTime);
            GyuStyfe.transform.position = Vector3.MoveTowards(GyuStyfe.transform.position, GyuStyfeStartPosition, MoveSpeed * Time.fixedDeltaTime);
        }    
        else if (EndMove) {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerEndPosition, MoveSpeed * Time.fixedDeltaTime);
            GyuStyfe.transform.position = Vector3.MoveTowards(GyuStyfe.transform.position, GyuStyfeEndPosition, MoveSpeed * Time.fixedDeltaTime);
        }
    }

    IEnumerator CheckIsDone() {
        

        yield return new WaitForSeconds(1f);

        StartCoroutine(PickUpLine.ShowText());

        while (!PickUpLine.isDone) {
            yield return new WaitForSeconds(Delay);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(RejectionLine.ShowText());

        while (!RejectionLine.isDone) {
            yield return new WaitForSeconds(Delay);
        }



        ContinueText.Play(animationToPlay);
        ContinueTextScript.isDisplayed = true;
    }
}
