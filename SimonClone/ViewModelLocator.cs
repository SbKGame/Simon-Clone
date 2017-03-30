using SimonClone.ViewModel;

namespace SimonClone
{
    public class ViewModelLocator
    {
        #region -- Properties --
        public MainViewModel Main
        {
            get { return IoCContainer.Resolve<MainViewModel>(); }
        }
        #endregion
    }
}
