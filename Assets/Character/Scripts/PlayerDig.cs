using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerDig : MonoBehaviour {
    [SerializeField] PlayerInAir PlayerInAir = null;

    public LayerMask LayerMask;

    // For placing grounds
    Tilemap SoftDirtMap;
    Tile DigDirtTile;
    public float Inventory = 0;

    readonly KeyCode DigOrPlace = KeyCode.Space;

    void Start() {
        SoftDirtMap = GameObject.FindGameObjectWithTag("Diggable").GetComponent<Tilemap>();
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

                        Inventory += 1;
                    }
                }
            }
            else {
                // Place ground
                Debug.Log("Place ground");

                if (Inventory > 0) {
                    Vector3Int cellPos = SoftDirtMap.WorldToCell(transform.position);
                    cellPos.y -= 1;
                    Debug.Log(transform.position.y);
                    SoftDirtMap.SetTile(cellPos, DigDirtTile);

                    Inventory--;
                }
            }
        }
    }
}
