using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public Transform StartPoint;
    public GameObject HeroPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IInputService>().To<Input—omputerService>().FromInstance(new Input—omputerService()).AsSingle();
        Container.Bind<StateMachineEvents<Character>>().FromInstance(new StateMachineEvents<Character>()).AsSingle();
         
        Character hero = Container.InstantiatePrefabForComponent<Character>(HeroPrefab, StartPoint.position, Quaternion.identity, null);
        Container.Bind<Character>().FromInstance(hero).AsSingle();
         
    }


}
