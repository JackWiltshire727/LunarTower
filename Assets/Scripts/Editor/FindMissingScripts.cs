using UnityEditor;
using UnityEngine;

public class FindMissingScripts
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    public static void FindMissing()
    {
        // Use FindObjectsByType with no sorting for performance
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject go in allObjects)
        {
            Component[] comps = go.GetComponents<Component>();
            for (int i = 0; i < comps.Length; i++)
            {
                if (comps[i] == null)
                {
                    Debug.Log(go.name + " has a missing script in component #" + i, go);
                }
            }
        }
    }
}
