using ITI.DesignPatterns.Foundation.Runtime.AssetManagement;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ITI.DesignPatterns.Foundation.Editor
{
    [CustomEditor(typeof(AssetCatalog), true)]
    public class ImageIdsEnumGenerator : UnityEditor.Editor
    {
        private const string EnumName = "ImageIdsEnum";
        private const string Path = "../Packages/ITI.DesignPatterns.CustomPackage/Runtime/ImageIdsEnum.cs";

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Generate Enum From All ImageSchemas"))
            {
                GenerateEnum();
            }
        }

        private void GenerateEnum()
        {
            var guids = AssetDatabase.FindAssets(filter: "t:ImageSchema");

            var scriptableObjects = guids.Select(guid =>
                    AssetDatabase.LoadAssetAtPath<ImageSchema>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(image => image != null && !string.IsNullOrEmpty(image.Id))
                .ToList();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("// Auto-Generated enum from ImageSchema Assets");
            stringBuilder.AppendLine("namespace DesignPatterns");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"public enum {EnumName}");
            stringBuilder.AppendLine("  {");

            foreach (var scriptableObject in scriptableObjects)
            {
                var safeId = MakeSafeFromEnum(scriptableObject.Id);

                stringBuilder.AppendLine($"     {safeId},");
            }

            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("}");

            File.WriteAllText(Path, stringBuilder.ToString());
            AssetDatabase.Refresh();

            Debug.Log($"Enum {EnumName} generated at {Path} with {scriptableObjects.Count} entries.");
        }

        private static string MakeSafeFromEnum(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "_empty";

            var safe = id.Replace(" ", "_").Replace("-", "_");

            safe = new string(safe.Where(character => char.IsLetterOrDigit(character) || character == '_').ToArray());

            if (char.IsDigit(safe[0]))
                safe = "_" + safe;

            return safe;
        }
    }
}