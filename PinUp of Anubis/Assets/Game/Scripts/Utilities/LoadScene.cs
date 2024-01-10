using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    [OPS.Obfuscator.Attribute.DoNotRename]
    public static void LoadNextScene() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadSceneAsync(nextSceneIndex);
        } else {
            Debug.LogWarning("Next scene index is out of range.");
        }
    } 

    [OPS.Obfuscator.Attribute.DoNotRename]
    public static void LoadPreviousScene() {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (previousSceneIndex >= 0) {
            SceneManager.LoadSceneAsync(previousSceneIndex);
        } else {
            Debug.LogWarning("Previous scene index is out of range.");
        }
    } 

    [OPS.Obfuscator.Attribute.DoNotRename]
    public static void LoadSceneByIndex(int index) {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadSceneAsync(index);
        } else {
            Debug.LogWarning("Scene index " + index + " is out of range.");
        }
    } 

    [OPS.Obfuscator.Attribute.DoNotRename]
    public static void LoadSceneByRelativeIndex(int index) {
        int relativeSceneIndex = SceneManager.GetActiveScene().buildIndex + index;
        if (relativeSceneIndex >= 0 && relativeSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadSceneAsync(relativeSceneIndex);
        } else {
            Debug.LogWarning("Relative scene index " + relativeSceneIndex + " is out of range.");
        }
    } 
}
