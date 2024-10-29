using UnityEngine;

public class BlackHole : MonoBehaviour
{

    [SerializeField]
    private Transform headTransform;

    [SerializeField] 
    private float offsetFromHead;
    
    private static BlackHole instance;
    
    public static bool paused = true; // TODO: TEMPORARY FIX

    public static BlackHole GetInstance() {
        return instance;
    }

    public void Awake() {
        instance = this;
    }

    public void Update()
    {
        if (paused)
        {
            var thisTransform = transform;
            var position = thisTransform.position;
            position.y = headTransform.position.y + offsetFromHead;
            thisTransform.position = position;
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }
}
