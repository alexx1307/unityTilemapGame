using UnityEngine;

public interface FocusHolder
{
    FocusHolder DoActionOnHoverAndRetrieveNextFocus(Vector3 mousePosition);
    FocusHolder DoActionOnClickAndRetrieveNextFocus(Vector3 mousePosition);
    void LoseFocus();
    void GetFocus();
}