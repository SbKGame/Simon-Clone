using GalaSoft.MvvmLight.Command;
using SbKGame.Core.Scene;
using System.Windows.Threading;

namespace SimonClone.Scene
{
    public class StartMenuScene : SceneBase
    {
        #region -- Commands --
        public RelayCommand StartGameCommand { get; private set; }
        public RelayCommand ExitGameCommand { get; private set; }
        #endregion

        #region -- Constructors --
        public StartMenuScene()
        {
            StartGameCommand = new RelayCommand(() => SceneManager.GoToScene(IoCContainer.Resolve<GameScene>()));
            ExitGameCommand = new RelayCommand(Dispatcher.CurrentDispatcher.InvokeShutdown);
        }
        #endregion
    }
}
