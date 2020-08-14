using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using SixThreeZero.UI;
[AddComponentMenu("630Studios/ColorPicker/Demo_000")]
    public class ColorPickerDemo_000: MonoBehaviour
    {
        public MeshFilter TargetMeshFilter;
        private Transform targetMeshTransform;
        public Button btnSelectColor;

        
        private Color32 currentColor = Color.white;

        public void Awake()
        {
            SetMeshVertexColor(currentColor);
            btnSelectColor.onClick.AddListener(btnSelectColor_Click);
            targetMeshTransform = TargetMeshFilter.transform;
        }

        public void Update()
        {
            targetMeshTransform.Rotate(0, Mathf.Sin(Time.time), 0);
        }

        public void SetMeshVertexColor(Color32 color)
        {
            currentColor = color;
            Mesh mesh = TargetMeshFilter.sharedMesh;
            TargetMeshFilter.sharedMesh = null;
            

            Color32[] colors = mesh.colors32;
            if (colors.Length < 1)
                colors = new Color32[mesh.vertices.Length];

            for (int i = 0; i < colors.Length; i++)
                colors[i] = color;

            mesh.colors32 = colors;
            TargetMeshFilter.sharedMesh = mesh;
        }

        public void btnSelectColor_Click()
        {
            Color32 originalColor = currentColor;
            UI_ColorPicker.Display(currentColor,false, false, SetMeshVertexColor, SetMeshVertexColor, () => SetMeshVertexColor(originalColor));

        }

        public void btnSelectColorWChart_Click()
        {
            Color32 originalColor = currentColor;
            UI_ColorPicker.Display(currentColor,true, false, SetMeshVertexColor, SetMeshVertexColor, () => SetMeshVertexColor(originalColor));

        }

        public void btnSelectColorWChatAndHistory_Click()
        {
            Color32 originalColor = currentColor;
            UI_ColorPicker.Display(currentColor,true, true, SetMeshVertexColor, SetMeshVertexColor, () => SetMeshVertexColor(originalColor));

        }
    }
