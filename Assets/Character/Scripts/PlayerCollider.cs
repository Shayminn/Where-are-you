using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer = null;

    [SerializeField] PlayerDig PlayerDig = null;
    [SerializeField] PlayerMovement PlayerMovement = null;

    [SerializeField] Tile SoftDirtTile = null;
    [SerializeField] Tilemap SoftDirtMap = null;
    public List<Vector3Int> SoftDirtTilesPos = new List<Vector3Int>();

    public Vector3 SavePoint;
    public int SavedInventory;

    public float ReviveDelay = 1f;

    bool Dead = false;

    void Start() {
        SoftDirtTile = Resources.Load<Tile>("DigDirt");    
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

        SoftDirtTilesPos.Clear();
        foreach (var pos in SoftDirtMap.cellBounds.allPositionsWithin) {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (SoftDirtMap.HasTile(localPlace)) {
                SoftDirtTilesPos.Add(localPlace);
            }
        }
    }

    public void ResetToSavePoint() {
        transform.position = SavePoint;

        PlayerDig.SetInventory(SavedInventory);
        
        SoftDirtMap.ClearAllTiles();
        foreach(Vector3Int pos in SoftDirtTilesPos) {
            SoftDirtMap.SetTile(pos, SoftDirtTile);
        }

        ResetOnDeath[] objsToReset = FindObjectsOfType<ResetOnDeath>();

        foreach(ResetOnDeath reset in objsToReset) {
            reset.ResetTrap();
        }
    }
}
