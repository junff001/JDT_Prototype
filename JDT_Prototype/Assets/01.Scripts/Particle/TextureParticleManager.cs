using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureParticleManager : MonoBehaviour
{
    public static TextureParticleManager Instance;
    private MeshParticleSystem _meshParticleSystem;
    private List<Paticle> _bloodList;
    private List<Paticle> _shellList;

    private void Awake()
    {
        _meshParticleSystem = GetComponent<MeshParticleSystem>();
        Instance = this;
        _bloodList = new List<Paticle>();
        _shellList = new List<Paticle>();
    }

    private void Update()
    {
        for (int i = 0; i < _bloodList.Count; i++)
        {
            Paticle p = _bloodList[i];
            p.Update();
            if (p.IsComplete())
            {
                _bloodList.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < _shellList.Count; i++)
        {
            Paticle p = _shellList[i];
            p.Update();
            if (p.IsComplete())
            {
                _shellList.RemoveAt(i);
                //_meshParticleSystem.DestroyQuad(p.GetQuadIndex()); //�̰� ���߿� Ȱ��ȭ�ص� �ȴ�.
                i--;
            }
        }
    }

    public void SpawnShell(Vector3 pos, Vector3 dir)
    {
        int uvIndex = _meshParticleSystem.GetRandomShellIndex();
        float moveSpeed = Random.Range(1.5f, 2.5f);
        Vector3 quadSize = new Vector3(0.15f, 0.15f);
        float slowDownFactor = Random.Range(2f, 2.5f);
        _shellList.Add(new Paticle(pos, dir, _meshParticleSystem, uvIndex, moveSpeed, quadSize, slowDownFactor, isRotate: true));
    }

    public void SpawnBlood(Vector3 pos, Vector3 dir)
    {
        int uvIndex = _meshParticleSystem.GetRandomBloodIndex();
        float moveSpeed = Random.Range(0.3f, 0.5f);
        float sizeFactor = Random.Range(0.3f, 0.8f);
        Vector3 quadSize = new Vector3(1f, 1f) * sizeFactor;
        float slowDownFactor = Random.Range(0.8f, 1.5f);
        _bloodList.Add(new Paticle(pos, dir, _meshParticleSystem, uvIndex, moveSpeed, quadSize, slowDownFactor));
    }

    public class Paticle
    {
        private Vector3 _quadPosition;
        private Vector3 _direction;
        private MeshParticleSystem _meshParticleSystem;
        private int _quadIndex;
        private Vector3 _quadSize;
        private float _rotation;
        private int _uvIndex;

        private float _moveSpeed;
        private float _slowDownFactor;

        private bool _isRotate;

        public int GetQuadIndex()
        {
            return _quadIndex;
        }

        public Paticle(Vector3 pos, Vector3 direction, MeshParticleSystem meshParticleSystem, int uvIndex, float moveSpeed, Vector3 quadSize, float slowDownFactor, bool isRotate = false)
        {
            _quadPosition = pos;
            _direction = direction;
            _meshParticleSystem = meshParticleSystem;
            _isRotate = isRotate;
            _quadSize = quadSize;

            _rotation = Random.Range(0, 360f);

            _uvIndex = uvIndex;

            //_moveSpeed = Random.Range(0.3f, 0.5f);
            _moveSpeed = moveSpeed;
            _slowDownFactor = slowDownFactor;

            _quadIndex = _meshParticleSystem.AddQuad(_quadPosition, _rotation, _quadSize, true, _uvIndex);
        }

        public void Update()
        {
            _quadPosition += _direction * _moveSpeed * Time.deltaTime;

            //ź���� ��� ȸ���� �ؾ��ϴ�
            if (_isRotate)
                _rotation += 360f * (_moveSpeed * 0.1f) * Time.deltaTime;
            _meshParticleSystem.UpdateQuad(_quadIndex, _quadPosition, _rotation, _quadSize, true, _uvIndex);

            //�ӵ� �ٿ��ְ�
            _moveSpeed -= _moveSpeed * _slowDownFactor * Time.deltaTime;
        }

        public bool IsComplete()
        {
            return _moveSpeed < 0.05f;
        }
    }

}
