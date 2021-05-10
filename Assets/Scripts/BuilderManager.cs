using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuilderManager : MonoBehaviour
{
    public Transform buildButtonsPanel; 
    private Grid grid;
    private Building buidlingChosen = null;

    private GameObject buildingPreview;
    private Vector3Int? lastHoverPos = null;
    public Tilemap buildingsTilemap;

    public AudioClip placeHoverSound;
    public AudioSource audioSource;

    public GameObject buildButtonPrefab;

    public List<Building> possibleBuildings;

    // Start is called before the first frame update
    void Start()
    {
        grid = buildingsTilemap.GetComponentInParent<Grid>();
        foreach (Building building in possibleBuildings)
        {
            GameObject buildingButton = Instantiate(buildButtonPrefab);
            buildingButton.transform.SetParent(transform);
            buildingButton.GetComponent<Image>().sprite = building.Sprite;
            buildingButton.GetComponent<Button>().onClick.AddListener(() => PickBuilding(building));
            buildingButton.transform.SetParent(buildButtonsPanel);
            
        }

        buildingPreview = new GameObject();

        SpriteRenderer renderer = buildingPreview.AddComponent<SpriteRenderer>();

        renderer.color = new Color(1f,1f,1f,0.5f);
        renderer.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (buidlingChosen != null)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int position = grid.WorldToCell(worldPoint);
            position.z = 0;
            
            TileBase building = buildingsTilemap.GetTile(position);
            if (!position.Equals(lastHoverPos))
            {
                lastHoverPos = position;
                //audioSource.PlayOneShot(placeHoverSound);
                
                if (building == null){
                    buildingPreview.transform.position = grid.GetCellCenterWorld(position);
                     buildingPreview.SetActive(true);
                } else {
                    buildingPreview.SetActive(false);
                }

            }
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                if (building == null)
                {
                    PlaceBuilding(position);
                }
            }
        }
    }

    private void PlaceBuilding(Vector3Int position)
    {
        Debug.Log(position);
        buildingsTilemap.SetTile(position, buidlingChosen.Tile);
        TurnManager turnManager = FindObjectOfType<TurnManager>();
        Player currentPlayer = turnManager.CurrentPlayer;
        currentPlayer.UpdateResources(buidlingChosen.buildingCost);
        currentPlayer.AddPossesion(position, buidlingChosen);

        lastHoverPos = null;
        buidlingChosen = null;
        buildingPreview.SetActive(false);
    }

    public void PickBuilding(Building building)
    {
        buidlingChosen = building;
        buildingPreview.GetComponent<SpriteRenderer>().sprite = buidlingChosen.Sprite;
        buildingPreview.SetActive(true);
    }
}
