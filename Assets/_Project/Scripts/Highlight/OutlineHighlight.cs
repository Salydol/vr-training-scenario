using System.Collections.Generic;
using UnityEngine;

namespace VRTraining.Highlight
{
    // контур вокруг объекта
    // работает просто: к рендереру добавляется второй материал с шейдером контура
    // он рисует тот же меш чуть раздутым и одним цветом
    public class OutlineHighlight : MonoBehaviour, IHighlight
    {
        private static readonly int ColorId = Shader.PropertyToID("_OutlineColor");
        private static readonly int WidthId = Shader.PropertyToID("_OutlineWidth");

        [SerializeField] private Material outlineMaterial;
        [SerializeField] private Color color = new Color(1f, 0.8f, 0.1f);
        [SerializeField] private float width = 0.015f;
        [SerializeField] private Renderer[] renderers;

        private Material materialInstance;
        private bool isShown;

        private void Awake()
        {
            // если рендереры руками не указали, берем все что есть на объекте
            if (renderers == null || renderers.Length == 0)
                renderers = GetComponentsInChildren<Renderer>();

            // своя копия материала, чтобы цвет и толщина были у каждого объекта свои
            materialInstance = new Material(outlineMaterial);
            materialInstance.SetColor(ColorId, color);
            materialInstance.SetFloat(WidthId, width);
        }

        private void OnDestroy()
        {
            if (materialInstance != null)
                Destroy(materialInstance);
        }

        public void Show()
        {
            if (isShown)
                return;

            isShown = true;

            foreach (Renderer meshRenderer in renderers)
            {
                var materials = new List<Material>(meshRenderer.sharedMaterials);
                materials.Add(materialInstance);
                meshRenderer.sharedMaterials = materials.ToArray();
            }
        }

        public void Hide()
        {
            if (!isShown)
                return;

            isShown = false;

            foreach (Renderer meshRenderer in renderers)
            {
                var materials = new List<Material>(meshRenderer.sharedMaterials);
                materials.Remove(materialInstance);
                meshRenderer.sharedMaterials = materials.ToArray();
            }
        }
    }
}
