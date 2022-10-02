using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField, Range(0, 20)] private int nombreDePrefabs = 5;
    [SerializeField, Range(0, 10)] private float rayonDuCercle = 1.5f;
    [SerializeField, Range(-10, 10)] private float rotationSpeed = 1f;

    private List<GameObject> prefabs = new List<GameObject>();

    private int _nombreDePrefabs;
    private float _rayonDuCercle;
    private float _rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        InstantiatesPrefabs();
        // Keep track of variables' current values
        _nombreDePrefabs = nombreDePrefabs;
        _rayonDuCercle = rayonDuCercle;
        _rotationSpeed = rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // If variables' values have changed, instantiate prefabs
        if (nombreDePrefabs != _nombreDePrefabs || rayonDuCercle != _rayonDuCercle || rotationSpeed != _rotationSpeed) {
            _nombreDePrefabs = nombreDePrefabs;
            _rayonDuCercle = rayonDuCercle;
            _rotationSpeed = rotationSpeed;
            InstantiatesPrefabs();
        }

        // Rotate prefabs around the circle
        foreach (GameObject go in prefabs) {
            go.transform.RotateAround(Vector3.zero, Vector3.forward, rotationSpeed * 10 * Time.deltaTime);
        }
    }

    // Instantiates prefabs in a circle
    void InstantiatesPrefabs() {
        // Destroy all prefabs
        foreach (GameObject go in prefabs) {
            Destroy(go);
        }
        prefabs.Clear();

        // Instantiate new prefabs
        for (int i = 0; i < nombreDePrefabs; i++) {
            float angle = i * Mathf.PI * 2 / nombreDePrefabs + Mathf.PI / 2;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * rayonDuCercle;
            GameObject go = Instantiate(prefab, pos, Quaternion.identity, transform);
            go.transform.rotation = Quaternion.LookRotation(Vector3.forward, pos);
            prefabs.Add(go);
        }
    }
    
}
