using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using YandexMaps;

public class Sample : MonoBehaviour
{
    public RawImage image;
    public MapT.TypeMap typeMap;
    public MapT.TypeMapLayer mapLayer;

    public int size = 17;
    public int width = 640;
    public int height = 640;
    public float latitude = 52.249506f;
    public float longitude = 104.263297f;

    public List<string> markers = new List<string>();
    public List<string> zone = new List<string>();

    private int countMarkers = 0;

    private int sizeLast = 17;
    private int widthLast = 640;
    private int heightLast = 640;
    private float latitudeLast = 52.249506f;
    private float longitudeLast = 104.263297f;
    private MapT.TypeMap typeMapLast;
    private MapT.TypeMapLayer mapLayerLast;

    private bool updateMap = true;

    public void LoadMap()
    {
        MapT.EnabledLayer = true;
        
        MapT.SetTypeMap = typeMap;
        MapT.SetTypeMapLayer = mapLayer;

        MapT.Size = size;
        MapT.Width = width;
        MapT.Height = height;
        MapT.Latitude = latitude;
        MapT.Longitude = longitude;

        MapT.SetMarker = markers;
        MapT.SetZone = zone;


        sizeLast = size;
        widthLast = width;
        heightLast = height;
        latitudeLast = latitude;
        longitudeLast = longitude;
        countMarkers = markers.Count;
        typeMapLast = typeMap;
        mapLayerLast = mapLayer;

        MapT.LoadMap();
        StartCoroutine(GetTexture());

        updateMap = true;
    }

    IEnumerator GetTexture()
    {
        yield return new WaitForSeconds(1f);
        image.texture = MapT.GetTexture;
    }


    private void Start()
    {
        updateMap = false;
        LoadMap();
    }


    void Update()
    {
        if (updateMap && (!Mathf.Approximately(longitudeLast, longitude) || !Mathf.Approximately(latitudeLast, latitude) || sizeLast != size || mapLayerLast != mapLayer || typeMapLast != typeMap || countMarkers != markers.Count))
        {

            updateMap = false;
            LoadMap();

        }
    }

    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isMove;
    public float moveSpeed = 5f;
    public float moveDuration = 1f;
    public void StartMove()
    {
        isMove = true;
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        StartCoroutine(Moving());
    }
    public void StopMove()
    {
        isMove = false;
    }

    IEnumerator Moving()
    {
        while (isMove)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 touchDelta = endPoint - startPoint;
            Vector3 targetPosition = image.transform.position + new Vector3(touchDelta.x, touchDelta.y, 0f);

            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                image.transform.position = Vector3.Lerp(image.transform.position, targetPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
    }

}