using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld Instance;
    
    [SerializeField] private LayerMask _mousePlaneLayerMask;

    private void Awake()
    {
        Instance = this;
    }

    public static Vector3 GetPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var raycastHit, float.MaxValue, Instance._mousePlaneLayerMask);
        return raycastHit.point;
    }
}
