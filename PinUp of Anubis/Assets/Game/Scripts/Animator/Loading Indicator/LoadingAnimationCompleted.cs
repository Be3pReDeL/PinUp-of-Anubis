using UnityEngine;

[OPS.Obfuscator.Attribute.DoNotObfuscateClass]
public class LoadingAnimationCompleted : StateMachineBehaviour {
    [OPS.Obfuscator.Attribute.DoNotRename]
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        LoadScene.LoadSceneByIndex(ChooseWhichToLoad.SceneIndex);
    }
}
