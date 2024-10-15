using UnityEngine;

public class DataUnitHolder : MonoBehaviour
{
    [field: SerializeField] public AnimationBaseController AnimationBaseController { get; private set; }
    [field: SerializeField] public ObjectSoundController ObjectSoundController { get; private set; }
    [field: SerializeField] public GameObject TargetObject { get; private set; }
    [field: SerializeField] public float TriggerDistance { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float RotateSpeed { get; private set; }
    [field: SerializeField] public float AttackTime { get; private set; }
    [field: SerializeField] public SoundConfig _soundConfig { get; private set; }


}
