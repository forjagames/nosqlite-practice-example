using UnityEngine;
using UnityEngine.Events;

public class DragAndDropCubeScript : MonoBehaviour
{
  private Color mouseOverColor = Color.blue;

  private Color originalColor;

  private bool isDragging = false;

  private float distance;

  private float initialYPosition;

  [SerializeField]
  private UnityEvent<GameObject> dragDrop;

  [SerializeField]
  private MeshRenderer meshRenderer;

  private void OnEnable()
  {
    this.originalColor = this.meshRenderer.material.color;
  }

  void OnMouseEnter()
  {
    this.meshRenderer.material.color = this.mouseOverColor;
  }

  void OnMouseExit()
  {
    this.meshRenderer.material.color = this.originalColor;
  }

  void OnMouseDown()
  {
    this.distance = Vector3.Distance(transform.position, Camera.main.transform.position);
    this.isDragging = true;
    this.initialYPosition = transform.position.y;
  }

  void OnMouseUp()
  {
    if (this.isDragging)
    {
      this.isDragging = false;
      this.meshRenderer.material.color = this.originalColor;

      this.dragDrop?.Invoke(gameObject);
    }
  }

  void Update()
  {
    if (this.isDragging)
    {
      this.initialYPosition = Mathf.MoveTowards(this.initialYPosition, 1f, Time.deltaTime * 4f);

      this.meshRenderer.material.color = this.mouseOverColor;

      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      Vector3 rayPoint = ray.GetPoint(distance);
      rayPoint.y = this.initialYPosition;
      transform.position = rayPoint;
    }
    else
    {
      transform.position = Vector3.MoveTowards(
        transform.position,
        new Vector3(transform.position.x, 0.25f, transform.position.z),
        Time.deltaTime * 2);
    }
  }
}
