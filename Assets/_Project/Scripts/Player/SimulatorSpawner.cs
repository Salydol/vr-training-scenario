using UnityEngine;
using UnityEngine.XR;

namespace VRTraining.Player
{
    // если шлем не подключен, включает симулятор чтобы можно было играть с клавы и мыши
    // с настоящим шлемом симулятор не создается и не мешает
    public class SimulatorSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject simulatorPrefab;

        private void Start()
        {
            if (XRSettings.isDeviceActive)
                return;

            Instantiate(simulatorPrefab);
        }
    }
}
