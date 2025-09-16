using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class ApplySpecificTagToChildren : EditorWindow
{
    private string targetTag = "Player";
    private bool includeInactive = true;

    [MenuItem("Tools/Apply Specific Tag To Children")]
    private static void ShowWindow()
    {
        GetWindow<ApplySpecificTagToChildren>("Apply Tag");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tag à rechercher", EditorStyles.boldLabel);
        targetTag = EditorGUILayout.TagField("Tag", targetTag);
        includeInactive = EditorGUILayout.Toggle("Inclure les objets inactifs", includeInactive);

        if (GUILayout.Button("Appliquer aux enfants (toute la scène)"))
        {
            ApplyTagToChildren(targetTag, includeInactive);
        }
    }

    private static void ApplyTagToChildren(string tagToFind, bool includeInactive)
    {
        var roots = SceneManager.GetActiveScene().GetRootGameObjects();
        int parentsFound = 0;
        int changed = 0;

        foreach (var root in roots)
        {
            ScanAndApply(root.transform, tagToFind, ref parentsFound, ref changed, includeInactive);
        }

        if (changed > 0)
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

        Debug.Log($"Parents trouvés avec le tag \"{tagToFind}\" : {parentsFound}. Enfants modifiés : {changed}.");
    }

    // Parcourt toute la hiérarchie et applique aux descendants dès qu'on trouve un parent avec le tag.
    private static void ScanAndApply(Transform current, string tag, ref int parentsFound, ref int changed, bool includeInactive)
    {
        if (!includeInactive && !current.gameObject.activeInHierarchy) return;

        if (current.CompareTag(tag))
        {
            parentsFound++;
            // applique le tag à tous les descendants de ce parent (récursif)
            foreach (Transform child in current)
                ApplyTagRecursively(child, tag, ref changed, includeInactive);

            // on retourne : pas besoin de re-scanner cette sous-arborescence (déjà traitée).
            return;
        }

        // sinon on continue à chercher plus bas
        foreach (Transform child in current)
            ScanAndApply(child, tag, ref parentsFound, ref changed, includeInactive);
    }

    private static void ApplyTagRecursively(Transform parent, string tag, ref int counter, bool includeInactive)
    {
        if (!includeInactive && !parent.gameObject.activeInHierarchy) return;

        if (!parent.CompareTag(tag))
        {
            Undo.RecordObject(parent.gameObject, "Apply Specific Tag");
            parent.tag = tag;
            counter++;
        }

        foreach (Transform child in parent)
            ApplyTagRecursively(child, tag, ref counter, includeInactive);
    }
}