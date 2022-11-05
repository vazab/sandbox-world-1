using UnityEngine;

public class ChildsToFaceCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Transform[] _childs;

    private void Start()
    {
        _childs = new Transform[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            _childs[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        if (_childs.Length == 0)
            return;

        foreach (Transform child in _childs)
        {
            child.LookAt(child.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        }
    }
}