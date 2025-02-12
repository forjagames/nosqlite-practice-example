using UnityEngine;
using UnityEngine.Events;

public class CubeGeneratorScript : MonoBehaviour
{
  [SerializeField]
  private GameObject prefab;

  [SerializeField]
  private UnityEvent<GameObject> onCreateNewCube;

  [SerializeField]
  [Range(0.5f, 2f)]
  private float randomDistance = 1f;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      Vector3 randomized = Random.onUnitSphere * randomDistance;

      var newInstance = Instantiate(prefab, prefab.transform.position + new Vector3(randomized.x, 0, randomized.z), Quaternion.identity);
      newInstance.SetActive(true);

      onCreateNewCube?.Invoke(newInstance);

      Debug.Log($"New cube instance created at {newInstance.transform.position}.");
    }
  }
}
