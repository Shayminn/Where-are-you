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
    Tile DigDirtTile;
    public int Inventory = 0;

    readonly KeyCode DigOrPlace = KeyCode.Space;

    void Start() {
        SoftDirtMap = GameObject.FindGameObjectWithTag("Diggable").GetComponent<Tilemap>();
        HardDirtMap = GameObject.FindGameObjectWithTag("Undiggable").GetComponent<Tilemap>();

        DigDirtTile = Resources.Load<Tile>("DigDirt");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(DigOrPlace)) {
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

                    TileBase tile = HardDirtMap.GetTile(cellPos);
                    if (tile == null) {
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
