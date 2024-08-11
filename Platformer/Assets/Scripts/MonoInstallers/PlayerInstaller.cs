using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{

    [Header("HeroPrefab")]
    [SerializeField] GameObject HeroPrefab;


    public override void InstallBindings()
    {
        InstallerHero();
        InstallerInputService();
        InstallerStateMachineEventsCharacter();
    }

    private void InstallerFactory()
    {
        Container.Bind<IFactory>().To<FactoryWithDiContainer>().FromInstance(new FactoryWithDiContainer(Container)).AsSingle();
    }


    private void InstallerHero()
    {
        //Character hero = Container.InstantiatePrefabForComponent<Character>(HeroPrefab, StartPoint.position, Quaternion.identity, null);
        Container.Bind<Character>().FromInstance(HeroPrefab.GetComponent<Character>()).AsSingle();
    }

   

    private void InstallerStateMachineEventsCharacter()
    {
        Container.Bind<StateMachineEvents<Character>>().FromInstance(new StateMachineEvents<Character>()).AsSingle();
    }

    private void InstallerInputService()
    {
        Container.Bind<InputService>().FromInstance(new InputService()).AsSingle();
    }
}

