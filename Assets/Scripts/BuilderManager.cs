using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class BuilderManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Grid grid;
    public Tilemap tilemap;
    public GameObject ButtonPrefab;
    public Transform Panel;
    private Building CurrentBuilding;
    [SerializeField] private GameObject Preview;
    
    void Start()
    {
        createButton();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentBuilding != null)
        {
            Vector3 pos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Preview.transform.position = grid.GetCellCenterWorld(Vector3Int.FloorToInt(pos));
            Preview.GetComponent<SpriteRenderer>().sprite = CurrentBuilding.Sprite;
        }

        if(Input.GetMouseButtonDown(0)){
            Preview.SetActive(true);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(worldPoint);
            Vector3Int position = grid.WorldToCell(worldPoint);
            Debug.Log(position);
            Debug.Log(CurrentBuilding);
            if (CurrentBuilding != null)
            {
                Preview.SetActive(false);
                tilemap.SetTile(position, CurrentBuilding.Tile);
                CurrentBuilding = null;
            }
        }
    }

    void createButton()
    {
        var buildings = GetComponentsInChildren<Building>();
        foreach (var building in buildings)
        {
            var Button = Instantiate(ButtonPrefab);
            Button.GetComponent<Image>().sprite = building.Sprite;
            Button.GetComponent<Button>().onClick.AddListener(() => pickBuilding(building));
            Button.transform.SetParent(Panel);
        }
        
    }

    private void pickBuilding(Building building)
    {
        CurrentBuilding = building;
    }
}
