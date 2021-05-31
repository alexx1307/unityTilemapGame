using UnityEngine;

public class Building : MonoBehaviour, FocusHolder {
    public BuildingType type;
    public Vector3Int position;
    public Player owner;


    public FocusHolder DoActionOnClickAndRetrieveNextFocus(Vector3 mousePosition)
    {
        return null;
    }

    public FocusHolder DoActionOnHoverAndRetrieveNextFocus(Vector3 mousePosition)
    {
        return this;
    }

    public void GetFocus()
    {
        type.OnFocus(this);
    }

    public void LoseFocus()
    {
        type.OnLoseFocus();
    }
}