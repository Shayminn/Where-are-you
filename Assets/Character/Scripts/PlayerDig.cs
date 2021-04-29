using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerDig : MonoBehaviour {
    [SerializeField] PlayerInAir PlayerInAir = null;
    [SerializeField] Text InventoryText = null;
    [SerializeField] float Distance = 0.3f;

    public LayerMask LayerMask;

    // For placing grounds
    Tilemap SoftDirtMap;
    Tilemap HardDirtMap;
    Tilemap TrapsMap;
    Tile DigDirtTile;
    public int Inventory = 0;

    public static KeyCode DigAndPlace = KeyCode.K;

    PauseUI PauseUI;

    void Start() {
        Transform smolGrid = GameObject.Find("SmolGrid").transform;
        SoftDirtMap = smolGrid.Find("SoftDirt").GetComponent<Tilemap>();
        HardDirtMap = smolGrid.Find("HardDirt").GetComponent<Tilemap>();
        TrapsMap = smolGrid.Find("Traps").GetComponent<Tilemap>();

        DigDirtTile = Resources.Load<Tile>("DigDirt");

        PauseUI = FindObjectOfType<PauseUI>();
    }

    // Update is called once per frame
    void Update() {
        if (!PauseUI.Opened) {
            if (Input.GetKeyDown(DigAndPlace)) {
                if (!PlayerInAir.InAir) {
                    // Dig ground
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Distance, LayerMask);

                    if (hit.collider != null) {
                        Tilemap map = hit.collider.GetComponent<Tilemap>();

                        if (map.CompareTag("Diggable")) {
                            Vector3Int cellPos = map.WorldToCell(hit.point);
                            cellPos.y -= 1;

                            map.SetTile(cellPos, null);

                            InventoryChange(1);
                        }
                    }
                }
                else {
                    // Place ground

                    if (Inventory > 0) {
                        Vector3Int cellPos = SoftDirtMap.WorldToCell(transform.position);
                        cellPos.y -= 1;

                        TileBase softTile = SoftDirtMap.GetTile(cellPos);
                        TileBase hardTile = HardDirtMap.GetTile(cellPos);

                        if (softTile == null && hardTile == null) {
                            SoftDirtMap.SetTile(cellPos, DigDirtTile);
                            InventoryChange(-1);
                        }
                    }
                }
            }
        }
    }

    void InventoryChange(int change) {
        Inventory += change;
        InventoryText.text = Inventory.ToString();
    }

    public void SetInventory(int inventory) {
        Inventory = inventory;
        InventoryText.text = Inventory.ToString();
    }
}
