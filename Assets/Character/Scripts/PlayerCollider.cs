using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer = null;

    [SerializeField] Rigidbody2D Rb2 = null;
    [SerializeField] PlayerDig PlayerDig = null;
    [SerializeField] PlayerMovement PlayerMovement = null;

    Tile SoftDirtTile = null;
    [SerializeField] Tilemap SoftDirtMap = null;
    public List<Vector3Int> SoftDirtTilesPos = new List<Vector3Int>();
    public List<Vector3> CollectiblePos = new List<Vector3>();

    public Vector3 SavePoint;
    public int SavedInventory;

    [SerializeField] GameObject CollectibleObj = null;
    public int SavedCollectible;

    public ResetOnDeath[] ObjsToReset;

    public float ReviveDelay = 1f;

    bool Dead = false;

    readonly KeyCode Reset = KeyCode.R;

    void Start() {
        SoftDirtTile = Resources.Load<Tile>("DigDirt");
    }

    void Update() {
        if (!Dead && Input.GetKeyDown(Reset)) {
            Die();
        }    
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (!Dead && collision.collider.CompareTag("Deadly")) {
            Die();
        }
    }

    public void Die() {
        Debug.Log("DEAD");

        Dead = true;
        DeathCounter.Instance.Increment();

        AudioManager.Instance.PlaySFX(AudioManager.SFX.Death);

        ChangeSpriteRendererColorAlpha(0);

        PlayerDig.enabled = false;
        PlayerMovement.enabled = false;

        Invoke(nameof(Revive), 1f);
    }

    public void Revive() {
        Dead = false;

        ChangeSpriteRendererColorAlpha(1);

        PlayerDig.enabled = true;
        PlayerMovement.enabled = true;

        ResetToSavePoint();
    }

    void ChangeSpriteRendererColorAlpha(float alpha) {
        Color color = SpriteRenderer.color;
        color.a = alpha;
        SpriteRenderer.color = color;
    }

    public void AssignSavePoint(Vector3 savePointPos) {
        SavePoint = savePointPos;

        SavedInventory = PlayerDig.Inventory;
        SavedCollectible = CollectibleCounter.CollectibleCollected;

        SoftDirtTilesPos.Clear();
        foreach (var pos in SoftDirtMap.cellBounds.allPositionsWithin) {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (SoftDirtMap.HasTile(localPlace)) {
                SoftDirtTilesPos.Add(localPlace);
            }
        }

        foreach (CollectibleItem heart in FindObjectsOfType<CollectibleItem>()) {
            CollectiblePos.Add(heart.transform.position);
        }

        ObjsToReset = FindObjectsOfType<ResetOnDeath>().Where(trap => !trap.Started).ToArray();
    }

    public void ResetToSavePoint() {
        transform.position = SavePoint;
        Rb2.velocity = Vector3.zero;

        PlayerDig.SetInventory(SavedInventory);
        CollectibleCounter.Instance.SetCollectibleCounter(SavedCollectible);

        SoftDirtMap.ClearAllTiles();
        foreach(Vector3Int pos in SoftDirtTilesPos) {
            SoftDirtMap.SetTile(pos, SoftDirtTile);
        }

        foreach(ResetOnDeath reset in ObjsToReset) {
            reset.ResetTrap();
        }

        CollectibleItem[] hearts = FindObjectsOfType<CollectibleItem>();
        foreach (Vector3 pos in CollectiblePos) {
            if (hearts.Length > 0) {
                CollectibleItem heart = hearts.FirstOrDefault(heart => heart.transform.position == pos);

                if (heart == null) {
                    Instantiate(CollectibleObj, pos, CollectibleObj.transform.rotation);
                }
            }
            else {
                Instantiate(CollectibleObj, pos, CollectibleObj.transform.rotation);
            }
        }

    }
}
