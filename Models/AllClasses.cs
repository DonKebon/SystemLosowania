using System.Collections.ObjectModel;

namespace SystemLosowania.Models
{
    internal class AllClasses
    {
        public ObservableCollection<SingleClass> All_Classes { get; set; } = new ObservableCollection<SingleClass>();
        public string ClassName { get; set; }

        public AllClasses() => LoadClasses();
        public void LoadClasses()
        {
            All_Classes.Clear();
            string appDataPath = FileSystem.AppDataDirectory;

            IEnumerable<SingleClass> allClasses = Directory
                .EnumerateFiles(appDataPath, $"*_class.txt")
                .Select(className => new SingleClass()
                {
                    ClassName = File.ReadLines(className).First()
                });

            foreach (SingleClass pingus in allClasses)
            {
                All_Classes.Add(pingus);
            }
        }
    }
}
