using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

namespace SOG.CVDFilter
{
    [CustomEditor(typeof(CVDFilter))]
    public class CVDFilterEditor : Editor
    {
        VisualElement root;
        VisualTreeAsset visualTree;
        StyleSheet styleSheet;

        CVDFilter cvdFilter;

        EnumField visionTypeEnum;
        VisualElement previewImage;
        Label descriptionLabel;

        public void OnEnable()
        {
            cvdFilter = (CVDFilter)target;

            // Each editor window contains a root VisualElement object
            root = new VisualElement();

            // Import UXML
            visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/CVDFilter/Editor/CVDFilterEditor.uxml");

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/CVDFilter/Editor/CVDFilterEditor.uss");
            if (styleSheet == null)
            {
                Debug.LogError("StyleSheet not found at the specified path");
                return;
            }
            root.styleSheets.Add(styleSheet);
        }

        public override VisualElement CreateInspectorGUI()
        {
            root.Clear();
            visualTree.CloneTree(root);

            visionTypeEnum = root.Q<EnumField>("VisionTypeEnum");
            previewImage = root.Q<VisualElement>("PreviewImage");
            descriptionLabel = root.Q<Label>("DescriptionLabel");

            if (cvdFilter == null)
            {
                Debug.LogError("cvdFilter is null");
                return root;
            }
            
            if (cvdFilter.SelectedVisionType.Equals(default(SOG.CVDFilter.VisionTypeInfo)))
            {
                Debug.LogError("SelectedVisionType is null");
                return root;
            }

            visionTypeEnum.Init(VisionTypeNames.Normal);
            visionTypeEnum.BindProperty(serializedObject.FindProperty("currentType"));

            visionTypeEnum.RegisterValueChangedCallback((e) =>
            {
                cvdFilter.ChangeProfile();
                if (cvdFilter.SelectedVisionType.previewImage != null)
                {
                    previewImage.style.backgroundImage = cvdFilter.SelectedVisionType.previewImage;
                }
                else
                {
                    Debug.LogError("previewImage is null");
                }

                if (cvdFilter.SelectedVisionType.description != null)
                {
                    descriptionLabel.text = cvdFilter.SelectedVisionType.description;
                }
                else
                {
                    Debug.LogError("description is null");
                }
            });

            return root;
        }
    }
}
