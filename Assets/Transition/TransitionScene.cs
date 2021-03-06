using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScene : MonoBehaviour {
    public static int CompletedLevel = 0;

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

    public GameObject EscapeText;

    bool WaitForInput = false;
    Coroutine SlowRead;

    void Awake() {
        GetLines();

        ContinueTextScript.SceneToChange = "Level " + (CompletedLevel + 1);
        GameObject gyuStyfe = Resources.Load<GameObject>("GyuStyfe" + CompletedLevel);
        GyuStyfe = Instantiate(gyuStyfe);
        GyuStyfeAnimator = GyuStyfe.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {
        PlayerAnimator.SetBool("Left", true);
        GyuStyfeAnimator.SetBool("Run", true);

        SlowRead = this.StartCoroutine(CheckIsDone());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            StartCoroutine(Skip());

            EscapeText.SetActive(false);
        }

        if (WaitForInput) {
            if (Input.anyKeyDown) {

                StartCoroutine(FadeOutText());

                PlayerAnimator.SetBool("Left", true);
                GyuStyfeAnimator.SetBool("Run", true);

                EndMove = true;
            }
        }
    }

    public IEnumerator Skip() {
        StopCoroutine(SlowRead);

        PickUpLine.Skip();
        if (PickUpLine.TypeWriteEffect != null) {
            StopCoroutine(PickUpLine.TypeWriteEffect);
        }

        RejectionLine.Skip();
        if (RejectionLine.TypeWriteEffect != null) {
            StopCoroutine(RejectionLine.TypeWriteEffect);
        }
            
        Player.transform.position = PlayerStartPosition;
        PlayerAnimator.SetBool("Left", false);

        GyuStyfe.transform.position = GyuStyfeStartPosition;
        GyuStyfeAnimator.SetBool("Run", false);
        StartMove = false;

        ContinueText.Play(animationToPlay, 0, 1);

        yield return new WaitForEndOfFrame();
        ContinueTextScript.isDisplayed = true;

        WaitForInput = true;
    }

    void FixedUpdate() {
        if (StartMove) {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerStartPosition, MoveSpeed * Time.fixedDeltaTime);
            if (Vector3.Distance(Player.transform.position, PlayerStartPosition) < 0.15f) {
                PlayerAnimator.SetBool("Left", false);
                StartMove = false;
            }

            GyuStyfe.transform.position = Vector3.MoveTowards(GyuStyfe.transform.position, GyuStyfeStartPosition, MoveSpeed * Time.fixedDeltaTime);
            if (Vector3.Distance(GyuStyfe.transform.position, GyuStyfeStartPosition) < 0.15f) {
                GyuStyfeAnimator.SetBool("Run", false);
                StartMove = false;
            }
        }
        else if (EndMove) {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerEndPosition, MoveSpeed * Time.fixedDeltaTime);
            GyuStyfe.transform.position = Vector3.MoveTowards(GyuStyfe.transform.position, GyuStyfeEndPosition, MoveSpeed * Time.fixedDeltaTime);
        }
    }

    IEnumerator CheckIsDone() {


        yield return new WaitForSeconds(1f);

        PickUpLine.TypeWriteEffect = StartCoroutine(PickUpLine.ShowText());

        while (!PickUpLine.isDone) {
            yield return new WaitForSeconds(Delay);
        }

        yield return new WaitForSeconds(2f);

        RejectionLine.TypeWriteEffect = StartCoroutine(RejectionLine.ShowText());

        while (!RejectionLine.isDone) {
            yield return new WaitForSeconds(Delay);
        }

        yield return new WaitForSeconds(1f);

        ContinueText.Play(animationToPlay);
        ContinueTextScript.isDisplayed = true;

        WaitForInput = true;
    }

    IEnumerator FadeOutText() {
        Text pickUpLineText = PickUpLine.GetComponent<Text>();
        Text rejectionLineText = RejectionLine.GetComponent<Text>();

        Color color = pickUpLineText.color;

        while (color.a > 0) {
            color.a -= Time.deltaTime;

            pickUpLineText.color = color;
            rejectionLineText.color = color;

            yield return null;
        }
    }

    void GetLines() {
        string[] lines = Lines.GenerateRandomLines();

        PickUpLine.Text.text = lines[0];
        RejectionLine.Text.text = lines[1];
    }
}
