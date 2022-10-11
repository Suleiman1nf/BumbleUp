using UnityEngine;

namespace Suli.Bumble
{
    public class Stair : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private Color _color;

        public Color Color => _color;

        public void ChangeColor(Color color)
        {
            _meshRenderer.material.color = color;
            _color = color;
        }
    }
}