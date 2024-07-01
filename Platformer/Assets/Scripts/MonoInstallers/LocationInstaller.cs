using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public Transform StartPoint;
    public GameObject HeroPrefab;

    public HealthPointView HealthPointView;
    public override void InstallBindings()
    {
        Container.Bind<IInputService>().To<Input—omputerService>().FromInstance(new Input—omputerService()).AsSingle();

        Container.Bind<StateMachineEvents<Character>>().FromInstance(new StateMachineEvents<Character>()).AsSingle();

        Container.Bind<HealthPointView>().FromComponentInNewPrefab(HealthPointView).AsSingle();

        Character hero = Container.InstantiatePrefabForComponent<Character>(HeroPrefab, StartPoint.position, Quaternion.identity, null);
        Container.Bind<Character>().FromInstance(hero).AsSingle();

        var a = hero.gameObject.GetComponent<HealthPoint>();
        a.OnHealthChange += (p,n) => HealthPointView.ViewData(n / (float)a.MaxValue);
    }


}
