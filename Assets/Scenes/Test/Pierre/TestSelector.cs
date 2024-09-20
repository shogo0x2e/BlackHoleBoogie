using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler : MonoBehaviour
{
    // This method will be called when the object is selected
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(gameObject.name + " was selected!");
        // Call your custom logic here
    }
    
}
