using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuilderManager : MonoBehaviour, FocusHolder
{
    
    private Grid grid;
    private BuildingType buidlingChosen = null;

    private GameObject buildingPreview;
    private Vector3Int? lastHoverPos = null;
    public Tilemap buildingsTilemap;

    public AudioClip placeHoverSound;
    public AudioSource audioSource;

    private Transform detailsPanel; 
    private GameObject buildButtonPrefab;

    public List<BuildingType> possibleBuildings;

    // Start is called before the first frame update
    void Start()
    {
        grid = buildingsTilemap.GetComponentInParent<Grid>();


        buildingPreview = new GameObject();

        SpriteRenderer renderer = buildingPreview.AddComponent<SpriteRenderer>();

        renderer.color = new Color(1f,1f,1f,0.5f);
        renderer.sortingOrder = 1;

        detailsPanel = GameObject.Find("DetailsPanel").transform;
        buildButtonPrefab = (GameObject)Resources.Load("Prefabs/buildButton", typeof(GameObject));;
    }

     private void PlaceBuilding(Vector3Int position)
    {
        Debug.Log(position);
        buildingsTilemap.SetTile(position, buidlingChosen.Tile);
        TurnManager turnManager = FindObjectOfType<TurnManager>();
        Player currentPlayer = turnManager.CurrentPlayer;
        currentPlayer.UpdateResources(buidlingChosen.buildingCost);

        GameObject go = new GameObject("building");
        Building building = go.AddComponent<Building>();
        building.type = buidlingChosen;
        building.position = position;
        building.owner = currentPlayer;
        MapItemsManager.Instance.AddBuilding(building);

        lastHoverPos = null;
        buidlingChosen = null;
        buildingPreview.SetActive(false);
    }

    public void PickBuilding(BuildingType building)
    {
        buidlingChosen = building;
        buildingPreview.GetComponent<SpriteRenderer>().sprite = buidlingChosen.Sprite;
        buildingPreview.SetActive(true);
    }

    public FocusHolder DoActionOnHoverAndRetrieveNextFocus(Vector3 mousePosition)
    {
        if (buidlingChosen != null)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = 0;
            Vector3Int position = grid.WorldToCell(worldPoint);
            
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
        }
        return this;
    }

    public FocusHolder DoActionOnClickAndRetrieveNextFocus(Vector3 mousePosition)
    {
        if (buidlingChosen != null)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = 0;
            Vector3Int position = grid.WorldToCell(worldPoint);
            
            TileBase building = buildingsTilemap.GetTile(position);
            if (!EventSystem.current.IsPointerOverGameObject() )
            {
                if (building == null)
                {
                    PlaceBuilding(position);
                }
            }
        }
        return null;
    }

    public void LoseFocus()
    {
         foreach (Transform child in detailsPanel) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void GetFocus()
    {
        foreach (BuildingType building in possibleBuildings)
        {
            GameObject buildingButton = Instantiate(buildButtonPrefab);
            buildingButton.transform.SetParent(transform);
            buildingButton.GetComponent<Image>().sprite = building.Sprite;
            buildingButton.GetComponent<Button>().onClick.AddListener(() => PickBuilding(building));
            buildingButton.transform.SetParent(detailsPanel);
            
        }
    }
}
