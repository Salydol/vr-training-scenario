using UnityEngine;
using UnityEngine.XR;

namespace VRTraining.Player
{
    // если шлем не подключен, включает симулятор чтобы можно было играть с клавы и мыши
    // с настоящим шлемом симулятор не создается и не мешает
    public class SimulatorSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject simulatorPrefab;

        // симулятор один на всю игру и переезжает между сценами
        private static GameObject simulator;

        private void Start()
        {
            if (XRSettings.isDeviceActive || simulator != null)
                return;

            simulator = Instantiate(simulatorPrefab);
            DontDestroyOnLoad(simulator);
        }
    }
}
