using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshParticleSystem : MonoBehaviour
{
    private const int MAX_QUAD_AMOUNT = 15000;
    //�����Ϳ��� ��������Ʈ�� �ִ� �ȼ� ��ǥ���� �����ϵ��� 
    [Serializable]
    public struct ParticleUVPixels
    {
        public Vector2Int uv00Pixels;
        public Vector2Int uv11Pixels;
    }

    //�ؽ��ĳ����� ��ֶ����� �� uv ��ǥ���� ������ �ִ� �迭
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

    //�޽� ������ �����ؾ� �ϴ����� üũ�ϴ� ����
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

        //�޽��� ��� �ٿ�尡 ������ Ư�� ��ǥ�̻� ȭ�� ������ ������ �޽� ��ü�� �ȱ׷����� ������ ����.
        _mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f);

        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;

        _meshRenderer = GetComponent<MeshRenderer>();
        //����
        _meshRenderer.sortingLayerName = "Bottom_Effect";
        _meshRenderer.sortingOrder = 0;

        Material mat = _meshRenderer.material;
        Texture mainTexture = mat.mainTexture;

        int texWidth = mainTexture.width;
        int texHeight = mainTexture.height;

        //�Էµ� �ȼ� ��ǥ�� ��ֶ����� �� UV��ǥ�� �����Ѵ�.
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

        _uvCoordsArray = uvCoordsList.ToArray(); //����Ʈ�� �迭�� �ۼ�
    }
    public int GetRandomBloodIndex()
    {
        return Random.Range(0, 8);
    }

    public int GetRandomShellIndex()
    {
        return Random.Range(8, 9); //����� 8���� ź���̶�
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 mousePos = Input.mousePosition;
            //Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
            //pos.z = 0;
            //Ŭ������ ���ؼ� �����ϴ� ���ú�����
            //Vector3 quadPosition = pos;
            //Vector3 quadSize = new Vector3(0.3f, 0.3f);
            //float rotation = 0f;

            //int randomIdx = Random.Range(0, _uvCoordsArray.Length);

            //int spawnQuadIndex = AddQuad(quadPosition, rotation, quadSize, true, randomIdx);
            //���⼭ �� ���� �ε����� ������ � ���带 ������Ʈ ���� ������ �� �ִ�.


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
        //if (_quadIndex >= MAX_QUAD_AMOUNT) return 0; //�ִ�ġ�� �����ϸ� ���̻� ���� ���ϰ�

        UpdateQuad(_quadIndex, position, rotation, quadSize, skewed, uvIndex);

        int spawnedQuadIndex = _quadIndex;
        _quadIndex = (_quadIndex + 1) % MAX_QUAD_AMOUNT; //�����ϸ� ���� ���� ���尡 ��������.
        return spawnedQuadIndex;
    }

    public void UpdateQuad(int quadIndex, Vector3 position, float rotation, Vector3 quadSize, bool skewed, int uvIndex)
    {
        //Relocate vertices
        //�簢���ϳ��� 4���� ���� �ʿ��ؼ� 4���� �����
        int vIndex = quadIndex * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        //������ �������� ȸ����Ŵ - ȸ���� �ð���� ȸ���� 
        // skewed�� true�ϰ�� ũ�⸦ ��� ��Ų��
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
         *   ������ �簢���� ���ν�Ų��
         */



        //uV ���� - �븻������� ���� �����ϸ� ��.
        UVCoords uv = _uvCoordsArray[uvIndex];
        _uv[vIndex0] = uv.uv00;
        _uv[vIndex1] = new Vector2(uv.uv00.x, uv.uv11.y);
        _uv[vIndex2] = uv.uv11;
        _uv[vIndex3] = new Vector2(uv.uv11.x, uv.uv00.y);


        //Create Triangle
        int tIndex = quadIndex * 6; //�簢�� �ϳ��� 2���� �ﰢ���̰� �ﰢ���� 3���� �������̶�
        //Ʈ���̾ޱ� ������Ī�� �ð�������� �ؾ� ����
        _triangles[tIndex + 0] = vIndex0; // -1, -1
        _triangles[tIndex + 1] = vIndex1; // -1, 1
        _triangles[tIndex + 2] = vIndex2; // 1, 1

        _triangles[tIndex + 3] = vIndex0;  // -1, -1
        _triangles[tIndex + 4] = vIndex2;  // 1, 1
        _triangles[tIndex + 5] = vIndex3;  // 1, -1

        //������ ������ �� ��ݿ������� ������ ����������� �����ϰ� ȭ�鿡 �ݿ����� �ʴ´�.
        //������ �� �۾��� �� �����ӿ� �������� ���忡 �� ���ִϱ� ������ �۾��� �����ӿ� ������ �̷�����. �׷��� boolean ������ ���� �ѹ���
        // TextMeshPro�� ForceMeshUpdate�� �� �����̴�.
        _updateVertices = true;
        _updateUV = true;
        _updateTriangles = true;
    }

    //Ȥ�ö� ���带 ���־��� ���� �ִٸ� �̰� ���
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
