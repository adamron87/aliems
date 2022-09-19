using UnityEngine;

public class PlaceRock : MonoBehaviour
{

    RaycastHit hit;
    Vector3 movePoint;
    public GameObject rockPrefab;
    public GameObject bridgePrefab;
    public GameObject ghostBridgePrefab;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] Vector3 tempOffset = new Vector3(0,0,0);   
    public Vector3 result;
    public float rotation;
    private float objectSpawnedAmount = 0;
    private GameObject currentGhost;
    [SerializeField] Vector3 rotateBridge = new Vector3(0,0,0);
    public GameObject objectOneLocation;
    public GameObject parentObject;
    public CreateZenObject[] zenObjects;
    public int currentIndex;
    public float currentAngle;
    public Quaternion rotateGhostBridge;
    public Quaternion fixBridge;
    public int spinTime = 2;
    public Vector3 lastKnownPosition;
    public Quaternion quaternionLocation;
    private GameObject bridgeGameObject;
    
    public void Update()
    {
      //  bridgeGameObject = transform.Find("ghostBridge").gameObject;

        float xRot = 0f;
        float yRot = 0f;
        float zRot = 0f;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // creating the offset for the Y value, to move objects off the spawnLayer
        this.transform.position += tempOffset;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) 
        {
            transform.position = hit.point;
            if(currentGhost != null)
            {
                currentGhost.transform.position = hit.point + tempOffset;
            }
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            SpawnGhostBridge();
        }

        rotateGhostBridge = new Quaternion(xRot, currentAngle, yRot, currentGhost.transform.rotation.w);
        currentAngle = Mathf.Clamp(currentAngle, -1080 , 1080);
        
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            RotateBridgeLeft();
            xRot = transform.eulerAngles.x;
            yRot = transform.eulerAngles.y;
            zRot = transform.eulerAngles.z;
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            RotateBridgeRight();
            xRot = transform.eulerAngles.x;
            yRot = transform.eulerAngles.y;
            zRot = transform.eulerAngles.z;

        }
    
        if(Input.GetKeyUp(KeyCode.O))
        {
            SpawnBridge();
        }
    }

    public void RotateBridgeRight()
        {
            lastKnownPosition = new Vector3(currentGhost.transform.position.x, currentGhost.transform.position.y, currentGhost.transform.position.z);
            currentGhost.transform.Rotate(Vector3.up, spinTime * Time.deltaTime);
        }

    public void RotateBridgeLeft()
        {
            lastKnownPosition = new Vector3(currentGhost.transform.position.x, currentGhost.transform.position.y, currentGhost.transform.position.z);
            currentGhost.transform.Rotate(-Vector3.up, spinTime * Time.deltaTime);
        }
    

    public void SpawnBridge()
        {
            //rotateGhostBridge = new Quaternion(currentGhost.transform.rotation.x, currentAngle, currentGhost.transform.rotation.y, currentGhost.transform.rotation.w);
            currentIndex = 0;
            if(currentGhost != null)
                {
                    Destroy(currentGhost);
                }
            Instantiate(zenObjects[currentIndex].gameObject, lastKnownPosition, rotateGhostBridge);
            objectSpawnedAmount += 1;
        }

    public void SpawnGhostBridge()
        {
            currentIndex = 1;
            if(currentGhost != null)
                {
                    Destroy(currentGhost);
                }
            currentGhost = Instantiate(zenObjects[currentIndex].gameObject, transform.position + tempOffset, rotateGhostBridge);
            
        }

}
