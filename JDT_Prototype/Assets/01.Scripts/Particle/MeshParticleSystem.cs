using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshParticleSystem : MonoBehaviour
{

    private const int MAX_QUAD_AMOUNT = 15000;
    //에디터에서 스프라이트에 있는 픽셀 좌표값을 조정하도록 
    [Serializable]
    public struct ParticleUVPixels
    {
        public Vector2Int uv00Pixels;
        public Vector2Int uv11Pixels;
    }

    //텍스쳐내에서 노멀라이즈 된 uv 좌표값을 가지고 있는 배열
    private struct UVCoords
    {
        public Vector2 uv00;
        public Vector2 uv11;
    }

    [SerializeField] private ParticleUVPixels[] particleUVPixelsArray;
    private UVCoords[] _uvCoordsArray;

    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private Vector3[] _vertices;
    private Vector2[] _uv;
    private int[] _triangles;

    private int _quadIndex;

    //메시 정보를 갱신해야 하는지를 체크하는 변수
    private bool _updateVertices;
    private bool _updateUV;
    private bool _updateTriangles;

    private void Awake()
    {
        _mesh = new Mesh();

        _vertices = new Vector3[4 * MAX_QUAD_AMOUNT];
        _uv = new Vector2[4 * MAX_QUAD_AMOUNT];
        _triangles = new int[6 * MAX_QUAD_AMOUNT];

        _mesh.vertices = _vertices;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;

        //메시의 경계 바운드가 작으면 특정 좌표이상 화면 밖으로 나가면 메시 전체가 안그려지는 문제가 생김.
        _mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f);

        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;

        _meshRenderer = GetComponent<MeshRenderer>();
        //정렬
        _meshRenderer.sortingLayerName = "Bottom_Effect";
        _meshRenderer.sortingOrder = 0;

        Material mat = _meshRenderer.material;
        Texture mainTexture = mat.mainTexture;

        int texWidth = mainTexture.width;
        int texHeight = mainTexture.height;

        //입력된 픽셀 좌표를 노멀라이즈 된 UV좌표로 변경한다.
        List<UVCoords> uvCoordsList = new List<UVCoords>();
        foreach (ParticleUVPixels pixelUV in particleUVPixelsArray)
        {
            UVCoords uvCoords = new UVCoords
            {
                uv00 = new Vector2((float)pixelUV.uv00Pixels.x / texWidth, (float)pixelUV.uv00Pixels.y / texHeight),
                uv11 = new Vector2((float)pixelUV.uv11Pixels.x / texWidth, (float)pixelUV.uv11Pixels.y / texHeight)
            };
            //Debug.Log((float)pixelUV.uv11Pixels.x / texWidth + ", " + (float)pixelUV.uv11Pixels.y / texHeight);
            uvCoordsList.Add(uvCoords);
        }

        _uvCoordsArray = uvCoordsList.ToArray(); //리스트를 배열로 작성
    }
    public int GetRandomBloodIndex()
    {
        return Random.Range(0, 8);
    }

    public int GetRandomShellIndex()
    {
        return Random.Range(8, 9); //현재는 8번만 탄알이라
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 mousePos = Input.mousePosition;
            //Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
            //pos.z = 0;
            //클로저를 위해서 생성하는 로컬변수들
            //Vector3 quadPosition = pos;
            //Vector3 quadSize = new Vector3(0.3f, 0.3f);
            //float rotation = 0f;

            //int randomIdx = Random.Range(0, _uvCoordsArray.Length);

            //int spawnQuadIndex = AddQuad(quadPosition, rotation, quadSize, true, randomIdx);
            //여기서 이 쿼드 인덱스를 가지고 어떤 쿼드를 업데이트 할지 결정할 수 있다.


            //FunctionUpdater.Create(() =>
            //{
            //    quadPosition += new Vector3(1, 1) * 0.8f * Time.deltaTime;
            //    //quadSize += new Vector3(1, 1) * Time.deltaTime;
            //    //rotation += 360f * Time.deltaTime;
            //    UpdateQuad(spawnQuadIndex, quadPosition, rotation, quadSize, true, randomIdx);
            //});
        }
    }

    private void LateUpdate()
    {
        if (_updateVertices)
        {
            _mesh.vertices = _vertices;
            _updateVertices = false;
        }

        if (_updateUV)
        {
            _mesh.uv = _uv;
            _updateUV = false;
        }

        if (_updateTriangles)
        {
            _mesh.triangles = _triangles;
            _updateTriangles = false;
        }
    }

    public int AddQuad(Vector3 position, float rotation, Vector3 quadSize, bool skewed, int uvIndex)
    {
        //if (_quadIndex >= MAX_QUAD_AMOUNT) return 0; //최대치에 도달하면 더이상 생성 못하게

        UpdateQuad(_quadIndex, position, rotation, quadSize, skewed, uvIndex);

        int spawnedQuadIndex = _quadIndex;
        _quadIndex = (_quadIndex + 1) % MAX_QUAD_AMOUNT; //부족하면 가장 예전 쿼드가 지워진다.
        return spawnedQuadIndex;
    }

    public void UpdateQuad(int quadIndex, Vector3 position, float rotation, Vector3 quadSize, bool skewed, int uvIndex)
    {
        //Relocate vertices
        //사각형하나에 4개의 점이 필요해서 4개씩 배수로
        int vIndex = quadIndex * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        //원점을 기준으로 회전시킴 - 회전은 시계방향 회전임 
        // skewed가 true일경우 크기를 축소 시킨다
        if (skewed)
        {
            _vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(-quadSize.x, -quadSize.y);
            _vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(-quadSize.x, +quadSize.y);
            _vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(+quadSize.x, +quadSize.y);
            _vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(+quadSize.x, -quadSize.y);
        }
        else
        {
            _vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation - 180) * quadSize; // -1, -1
            _vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation - 270) * quadSize; // -1, 1
            _vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation - 0) * quadSize; // 1, 1
            _vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation - 90) * quadSize; // 1, -1
        }
        /*
         *   1 - 2
         *   |   |
         *   0 - 3
         *   순서로 사각형을 맵핑시킨다
         */



        //uV 맵핑 - 노말라이즈된 값을 맵핑하면 됨.
        UVCoords uv = _uvCoordsArray[uvIndex];
        _uv[vIndex0] = uv.uv00;
        _uv[vIndex1] = new Vector2(uv.uv00.x, uv.uv11.y);
        _uv[vIndex2] = uv.uv11;
        _uv[vIndex3] = new Vector2(uv.uv11.x, uv.uv00.y);


        //Create Triangle
        int tIndex = quadIndex * 6; //사각형 하나당 2개의 삼각형이고 삼각형은 3개의 꼭지점이라
        //트라이앵글 정점매칭은 시계방향으로 해야 정방
        _triangles[tIndex + 0] = vIndex0; // -1, -1
        _triangles[tIndex + 1] = vIndex1; // -1, 1
        _triangles[tIndex + 2] = vIndex2; // 1, 1

        _triangles[tIndex + 3] = vIndex0;  // -1, -1
        _triangles[tIndex + 4] = vIndex2;  // 1, 1
        _triangles[tIndex + 5] = vIndex3;  // 1, -1

        //변경이 생겼을 때 재반영해주지 않으면 변경없음으로 이해하고 화면에 반영하지 않는다.
        //하지만 이 작업을 매 프레임에 여러개의 쿼드에 다 해주니까 동일한 작업이 프레임에 여러번 이뤄진다. 그래서 boolean 변수를 통해 한번만
        // TextMeshPro의 ForceMeshUpdate가 이 역할이다.
        _updateVertices = true;
        _updateUV = true;
        _updateTriangles = true;
    }

    //혹시라도 쿼드를 없애야할 일이 있다면 이걸 사용
    public void DestroyQuad(int quadIndex)
    {
        int vIndex = quadIndex * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        _vertices[vIndex0] = Vector3.zero;
        _vertices[vIndex1] = Vector3.zero;
        _vertices[vIndex2] = Vector3.zero;
        _vertices[vIndex3] = Vector3.zero;

        _updateVertices = true;
    }
}
