using GalaSoft.MvvmLight;
using SbKGame.Core;

namespace SimonClone.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region -- Properties --
        public string Title
        {
            get { return "Simon Clone"; }
        }

        public ISceneManager SceneManager { get; private set; }
        #endregion

        #region -- Constructors --
        public MainViewModel(ISceneManager sceneManager)
        {
            SceneManager = sceneManager;
        }
        #endregion
    }
}
