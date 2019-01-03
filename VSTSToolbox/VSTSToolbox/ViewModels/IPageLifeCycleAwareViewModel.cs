namespace VSTSToolbox.ViewModels
{
    public interface IPageLifeCycleAwareViewModel
    {
        void OnAppearing();
        void OnDisappearing();
    }
}
