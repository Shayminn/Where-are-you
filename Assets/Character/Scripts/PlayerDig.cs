using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerDig : MonoBehaviour {
    [SerializeField] PlayerInAir PlayerInAir = null;
    [SerializeField] Text InventoryText = null;

    public LayerMask LayerMask;

    // For placing grounds
    Tilemap SoftDirtMap;
    Tilemap HardDirtMap;
    Tilemap TrapsMap;
    Tile DigDirtTile;
    public int Inventory = 0;

    readonly KeyCode DigOrPlace = KeyCode.LeftShift;
    readonly KeyCode DigOrPlace2 = KeyCode.RightShift;

    void Start() {
        Transform smolGrid = GameObject.Find("SmolGrid").transform;
        SoftDirtMap = smolGrid.Find("SoftDirt").GetComponent<Tilemap>();
        HardDirtMap = smolGrid.Find("HardDirt").GetComponent<Tilemap>();
        TrapsMap = smolGrid.Find("Traps").GetComponent<Tilemap>();

        DigDirtTile = Resources.Load<Tile>("DigDirt");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(DigOrPlace) || Input.GetKeyDown(DigOrPlace2)) {
            if (!PlayerInAir.InAir) {
                // Dig ground
                Debug.Log("Dig");
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, LayerMask);

                if (hit.collider != null) {
                    Tilemap map = hit.collider.GetComponent<Tilemap>();

                    if (map.CompareTag("Diggable"))
                    {
                        Debug.Log(map.tag);
                        Vector3Int cellPos = map.WorldToCell(hit.point);
                        cellPos.y -= 1;

                        map.SetTile(cellPos, null);

                        InventoryChange(1);
                    }
                }
            }
            else {
                // Place ground
                Debug.Log("Place ground");

                if (Inventory > 0) {
                    Vector3Int cellPos = SoftDirtMap.WorldToCell(transform.position);
                    cellPos.y -= 1;

                    TileBase softTile = SoftDirtMap.GetTile(cellPos);
                    TileBase hardTile = HardDirtMap.GetTile(cellPos);
                    TileBase trapTile = TrapsMap.GetTile(cellPos);

                    if (softTile == null && hardTile == null) {

                        //if (trapTile != null) {
                        //    TrapsMap.SetTile(cellPos, null);
                        //}

                        SoftDirtMap.SetTile(cellPos, DigDirtTile);
                        InventoryChange(-1);
                    }
                }
            }
        }
    }

    void InventoryChange(int change) {
        Inventory += change;
        InventoryText.text = Inventory.ToString();
    }
}
