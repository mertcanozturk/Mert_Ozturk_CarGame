using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class LevelCreator : MonoBehaviour
{
    [SerializeField]
    private Transform[] entrance;
    [SerializeField]
    private Transform[] target;

    public void GenerateLevel()
    {
        Debug.Log("It is working..");

        var obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        List<ObstacleInfo> obstacleList = new List<ObstacleInfo>();

        foreach (var obstacle in obstacles)
        {
            var go = PrefabUtility.GetCorrespondingObjectFromSource(obstacle);

            ObstacleInfo obstacleInfo = new ObstacleInfo()
            {
                position = obstacle.transform.position,
                rotation = obstacle.transform.eulerAngles,
                prefabPath = AssetDatabase.GetAssetPath(go).Split('/')[2] + '/' + AssetDatabase.GetAssetPath(go).Split('/')[3].Split('.')[0]
            };

            obstacleList.Add(obstacleInfo);
        }

        Vector3[] entranceList = new Vector3[8];
        Vector3[] targetList = new Vector3[8];

        for (int i = 0; i < 8; i++)
        {
            entranceList[i] = entrance[i].transform.position;
            targetList[i] = target[i].transform.position;
        }

        var scriptableObject = LevelData.CreateInstance("LevelData");
        LevelData levelData = scriptableObject as LevelData;
        levelData.entrancePoints = entranceList;
        levelData.targetPoints = targetList;
        levelData.Obstacles = obstacleList;
        string path = "Assets/Resources/LevelDatas/LevelData2.asset";
        AssetDatabase.CreateAsset(scriptableObject, path);

        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = scriptableObject;
    }

}

[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelCreator creator = (LevelCreator)target;

        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("entrance");
        SerializedProperty stringsProperty1 = so.FindProperty("target");
        EditorGUILayout.PropertyField(stringsProperty, true);
        EditorGUILayout.PropertyField(stringsProperty1, true);

        so.ApplyModifiedProperties(); 
        GUILayout.Space(10);
        if (GUILayout.Button("Create Level"))
            creator.GenerateLevel();

    }
}

#endif