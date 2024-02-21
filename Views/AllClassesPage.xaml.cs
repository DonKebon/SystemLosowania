using SystemLosowania.Models;

namespace SystemLosowania.Views;

public partial class AllClassesPage : ContentPage
{
	public AllClassesPage()
	{
		InitializeComponent();

        BindingContext = new AllClasses();

    }
    protected override void OnAppearing()
    {
        ((AllClasses)BindingContext).LoadClasses();
    }

    private async void AddClass_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is AllClasses _class)
        {
            if(!String.IsNullOrEmpty(_class.ClassName))
            {
                string appDataPath = FileSystem.AppDataDirectory;

                string fileName = $"{_class.ClassName}_class.txt";
                //`Shell.Current.DisplayAlert("Alert", _class.ClassName, "ok");

                File.WriteAllText(Path.Combine(appDataPath, fileName), _class.ClassName);
                await Shell.Current.GoToAsync($"{nameof(ClassPage)}?{nameof(ClassPage.ClassName)}={_class.ClassName}");
            }
        }
    }
    private async void ClassChoice(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var _class = (SingleClass)e.CurrentSelection[0];
            await Shell.Current.GoToAsync($"{nameof(ClassPage)}?{nameof(ClassPage.ClassName)}={_class.ClassName}");
            collectionViewClasses.SelectedItem = null;
        }
    }
}