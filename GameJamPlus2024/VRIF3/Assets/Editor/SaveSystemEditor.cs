using UnityEditor;
using CI.QuickSave;

public class SaveSystemEditor
{
    [MenuItem("Dandelian Tools/Delete Dev Save")]
    public static void DeleteSave()
    {
        QuickSaveWriter.DeleteRoot("Player");
    }
}
