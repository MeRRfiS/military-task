using Military.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Military.Scripts.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainMenu>().AsSingle();
        }
    }
}