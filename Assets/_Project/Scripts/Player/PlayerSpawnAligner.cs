using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace VRTraining.Player
{
    // при загрузке сцены ставит игрока ровно в точку спавна и лицом вперед
    // без этого после перехода между сценами голова остается смещена и повернута
    // так же как игрок стоял в прошлой сцене, и меню может оказаться за спиной
    public class PlayerSpawnAligner : MonoBehaviour
    {
        private IEnumerator Start()
        {
            XROrigin origin = GetComponent<XROrigin>();

            if (origin == null)
                yield break;

            // запоминаем куда игрока поставили на сцене
            Vector3 spawnPosition = transform.position;
            Vector3 spawnForward = transform.forward;

            // ждем кадр, чтобы шлем успел отдать свое положение
            yield return null;

            // сначала поворот, потом позиция, высоту головы не трогаем
            origin.MatchOriginUpCameraForward(Vector3.up, spawnForward);

            Vector3 headPosition = spawnPosition;
            headPosition.y = origin.Camera.transform.position.y;
            origin.MoveCameraToWorldLocation(headPosition);
        }
    }
}
