using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _raycastDistance = 1f;

    [Header("Inventory")]
    [SerializeField] private InventoryManeger _inventoryManeger;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit;

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _raycastDistance, _layerMask))
            {
                if (hit.collider.TryGetComponent(out Item _item))
                {
                    if (_inventoryManeger.AddItem(_item.item, _item.amount))
                    {
                        Destroy(_item.gameObject);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_camera != null)
        {
            Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _raycastDistance, Color.red);
        }
    }
}