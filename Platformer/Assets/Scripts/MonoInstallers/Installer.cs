using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    [Header("StartPoint")]
    [SerializeField] Transform StartPoint;

    [Header("HeroPrefab")]
    [SerializeField] GameObject HeroPrefab;

    [Header("HealthPointView")]
    [SerializeField] HealthPointView HealthPointView;

    [Header("ProgressDialog")]
    [SerializeField] DialogPanel progressDialog;

    public override void InstallBindings()
    {
        InstallerInputService();
        InstallerStateMachineEventsCharacter();
        InstallerHealthPointView();
        InstallerHero();
        InstallerProgressDialog();
        InstallerFactory();
    }

    private void InstallerFactory()
    {
        Container.Bind<IFactory>().To<FactoryWithDiContainer>().FromInstance(new FactoryWithDiContainer(Container)).AsSingle();
    }

    private void InstallerProgressDialog()
    {
        Container.Bind<DialogPanel>().FromInstance(progressDialog).AsSingle();
    }

    private void InstallerHero()
    {
        Character hero = Container.InstantiatePrefabForComponent<Character>(HeroPrefab, StartPoint.position, Quaternion.identity, null);
        Container.Bind<Character>().FromInstance(hero).AsSingle();
        var a = hero.gameObject.GetComponent<HealthPoint>();
        a.OnHealthChange += (p, n) => HealthPointView.ViewData(n / (float)a.MaxValue);
    }

    private void InstallerHealthPointView()
    {
        Container.Bind<HealthPointView>().FromInstance(HealthPointView).AsSingle();
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
