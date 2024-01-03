using UnityEngine;

public class BoxSelection : MonoBehaviour
{
    private Vector3 initialClickPosition;
    private Vector3 currentMousePosition;
    private bool isSelecting = false;

    void Update()
    {
        // Mouse sol tıklama işlemi
        if (Input.GetMouseButtonDown(0))
        {
            initialClickPosition = Input.mousePosition;
            isSelecting = true;
        }

        // Mouse sol tık basılı tutulduğu sürece kutu seçme işlemi
        if (Input.GetMouseButton(0))
        {
            currentMousePosition = Input.mousePosition;
        }

        // Mouse sol tık bırakıldığında seçilen nesneleri belirle
        if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
            SelectObjectsInBox();
        }
    }

    void OnDrawGizmos()
    {
        if (isSelecting)
        {
            DrawSelectionBox(initialClickPosition, currentMousePosition);
        }
    }

    void DrawSelectionBox(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Seçim kutusunu dünya koordinatlarına dönüştür
        Vector3 worldPosition1 = Camera.main.ScreenToWorldPoint(screenPosition1);
        Vector3 worldPosition2 = Camera.main.ScreenToWorldPoint(screenPosition2);

        // Draw a line between each pair of corners
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(worldPosition1.x, worldPosition1.y, Camera.main.nearClipPlane),
                        new Vector3(worldPosition2.x, worldPosition1.y, Camera.main.nearClipPlane));
        Gizmos.DrawLine(new Vector3(worldPosition2.x, worldPosition1.y, Camera.main.nearClipPlane),
                        new Vector3(worldPosition2.x, worldPosition2.y, Camera.main.nearClipPlane));
        Gizmos.DrawLine(new Vector3(worldPosition2.x, worldPosition2.y, Camera.main.nearClipPlane),
                        new Vector3(worldPosition1.x, worldPosition2.y, Camera.main.nearClipPlane));
        Gizmos.DrawLine(new Vector3(worldPosition1.x, worldPosition2.y, Camera.main.nearClipPlane),
                        new Vector3(worldPosition1.x, worldPosition1.y, Camera.main.nearClipPlane));
    }

    void SelectObjectsInBox()
{
    // Seçim kutusu içindeki nesneleri belirle
    Ray ray = Camera.main.ScreenPointToRay(initialClickPosition);
    RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

    // Seçili nesneleri işle
    foreach (RaycastHit hit in hits)
    {
        // Nesnenin Renderer bileşenini kontrol et
        Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            // İlgili nesneyi seçili olarak işle (örneğin, rengini değiştirebilirsiniz)
            renderer.material.color = Color.red;
        }
    }
}

}
