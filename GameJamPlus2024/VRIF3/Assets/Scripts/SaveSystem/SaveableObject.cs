using UnityEngine;
using System.Collections.Generic;

public class SaveableObject : MonoBehaviour
{
    public string objectID;

    private void Awake()
    {
        if (string.IsNullOrEmpty(objectID))
        {
            objectID = gameObject.name + "_" + System.Guid.NewGuid().ToString();
        }
    }

    public GameObjectState SaveState()
    {
        List<GameObjectState> childStates = new List<GameObjectState>();

        foreach (Transform child in transform)
        {
            SaveableObject childSaveable = child.GetComponent<SaveableObject>();
            if (childSaveable)
            {
                childStates.Add(childSaveable.SaveState());
            }
        }

        return new GameObjectState
        {
            objectName = objectID,
            position = transform.position,
            rotation = transform.rotation,
            children = childStates.ToArray()
        };
    }

    public void LoadState(GameObjectState state)
    {
        transform.position = state.position;
        transform.rotation = state.rotation;

        // Load children states recursively
        for (int i = 0; i < state.children.Length; i++)
        {
            GameObjectState childState = state.children[i];
            Transform childTransform = transform.GetChild(i);

            SaveableObject childSaveable = childTransform.GetComponent<SaveableObject>();
            if (childSaveable)
            {
                childSaveable.LoadState(childState);
            }
        }
    }
}

[System.Serializable]
public class GameObjectState
{
    public string objectName;
    public Vector3 position;
    public Quaternion rotation;
    public GameObjectState[] children;
}
