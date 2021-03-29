using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuilderManager : MonoBehaviour
{
    private Grid grid;
    private Building buidlingChosen = null;

    private GameObject hoverPlan;
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
        }

        hoverPlan = new GameObject();

        SpriteRenderer renderer = hoverPlan.AddComponent<SpriteRenderer>();

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
                audioSource.PlayOneShot(placeHoverSound);
                
                if (building == null){
                    hoverPlan.transform.position = grid.GetCellCenterWorld(position);
                     hoverPlan.SetActive(true);
                } else {
                    hoverPlan.SetActive(false);
                }

            }
            if (Input.GetMouseButtonDown(0))
            {
                if (building == null)
                {
                    Debug.Log(position);
                    buildingsTilemap.SetTile(position, buidlingChosen.Tile);
                    lastHoverPos = null;
                    buidlingChosen = null;
                    hoverPlan.SetActive(false);
                }
            }
        }
    }

    public void PickBuilding(Building building)
    {
        buidlingChosen = building;
        hoverPlan.GetComponent<SpriteRenderer>().sprite = buidlingChosen.Sprite;
        hoverPlan.SetActive(true);
    }
}
