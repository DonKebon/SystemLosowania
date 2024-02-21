using Microsoft.Maui.Controls.Handlers.Items;

namespace SystemLosowania
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //https://github.com/dotnet/maui/issues/14557
            CollectionViewHandler.Mapper.AppendToMapping("HeaderAndFooterFix", (_, collectionView) =>
            {
                collectionView.AddLogicalChild(collectionView.Header as Element);
                collectionView.AddLogicalChild(collectionView.Footer as Element);
            });

            MainPage = new AppShell();
        }
    }
}