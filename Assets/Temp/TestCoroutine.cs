using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class TestCoroutine : MonoBehaviour {
    [SerializeField] private Transform[] cubes; 
    
    private async Task Start() {
        //StartCoroutine(Test());
        await TestAsync();
    }
    private IEnumerator Test() {
        Debug.Log("start");
        while (true) {
            for (int i = 0; i < cubes.Length; i++) {
              cubes[i].Rotate(new Vector3(0,1,1), 5);
              yield return null;
            }
        }
    }

    private async Task TestAsync() {
        while (true) {
            for (int i = 0; i < cubes.Length; i++) {
                cubes[i].Rotate(new Vector3(0,1,1), 5);
                await Task.Yield();
            }
        }
    }
    
}
