using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] public GameObject goTo;
    [SerializeField] float Speed = 10;
    private void Start()
    {
        Vector3 goToPosition = goTo.transform.position;
        transform.position = new Vector3(goToPosition.x, goToPosition.y, -10);
    }

    private void FixedUpdate()
    {
        Vector3 goToPosition = goTo.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(goToPosition.x, goToPosition.y, -10), Speed*Time.deltaTime);
    }
}
