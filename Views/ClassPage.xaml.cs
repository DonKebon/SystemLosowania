using SystemLosowania.Models;
using System.Collections.ObjectModel;
using Path = System.IO.Path;
using Microsoft.Maui.Controls.Shapes;
using System;

namespace SystemLosowania.Views;

[QueryProperty(nameof(ClassName), nameof(ClassName))]
public partial class ClassPage : ContentPage
{
    public ClassPage()
    {
        InitializeComponent();

        BindingContext = new Person();
        collectionViewPersons.ItemsSource = Persons;

        Random random = new Random();
        HappyNumberValue = random.Next(0, 36);
        HappyNumber.Text = HappyNumberValue.ToString();
    }
    private ObservableCollection<Person> Persons { get; set; } = new ObservableCollection<Person>();

    private int HappyNumberValue = 0;
    private string className;
    private int MaxCount;

    public string ClassName
    {
        get
        {
            return className;
        }
        set
        {
            if (value != null)
            {
                className = value;
                LoadClass(value);
            }
        }
    }
    private void LoadClass(string className)
    {
        MaxCount = 1;
        string appDataPath = FileSystem.AppDataDirectory;
        string personsFromFile = File.ReadAllText(Path.Combine(appDataPath, $"{className}_class.txt"));
        string[] separatedPersons = personsFromFile.Split("\r\n");
        separatedPersons = separatedPersons.Skip(1).ToArray();

        foreach (string PersonLine in separatedPersons)
        {
            string[] product = PersonLine.Split(';');

                Person readyPerson = new Person()
                {
                    Count = MaxCount,
                    PersonName = product[0]
                };
            MaxCount++;
            Persons.Add(readyPerson);
        };
    }
    public void AddPerson_Clicked(object sender, EventArgs e)
    {

        string fileName = $"{this.ClassName}_class.txt";
        var context = ((Person)BindingContext);
        if (!String.IsNullOrEmpty(context.PersonName))
        {
            string addPerson;

            addPerson = $"\r\n{context.PersonName};true";
            File.AppendAllText(Path.Combine(FileSystem.AppDataDirectory, fileName), addPerson);

            Person newPerson = new Person()
            {
                Count = MaxCount,
                PersonName = context.PersonName
            };
            MaxCount++;

            Persons.Add(newPerson);
        }
    }

    public void RandomPerson_CLicked(object sender, EventArgs e)
    {
        string personsFromFile = File.ReadAllText(Path.Combine(FileSystem.AppDataDirectory, $"{className}_class.txt"));
        string[] separatedPersons = personsFromFile.Split("\r\n");
        separatedPersons = separatedPersons.Skip(1).ToArray();

        List<string> EveryPersonName = new List<string>();

        foreach (string PersonLine in separatedPersons)
        {
            string[] product = PersonLine.Split(';');

            EveryPersonName.Add(product[0]);

        };

        Random random = new Random();
        int randomIndex;

        do
        {
            randomIndex = random.Next(0, EveryPersonName.Count);
        } while (randomIndex == HappyNumberValue);

        string RandomPerson = EveryPersonName[randomIndex];

        Shell.Current.DisplayAlert("Wylosowano osobê: ", RandomPerson, "ok");

    }

    private async void ClassDelete_Clicked(object sender, EventArgs e)
    {
        File.Delete(FileSystem.AppDataDirectory + $"/{this.className}_class.txt");
        await Shell.Current.GoToAsync("..");
    }

    private async void PersonEdit(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var _person = (Person)e.CurrentSelection[0];
            string personName = _person.PersonName;
            collectionViewPersons.SelectedItem = null;

            string NewPersonName = await DisplayPromptAsync($"Edytujesz ucznia: {personName}", "", initialValue: $"{personName}");

            if (!string.IsNullOrEmpty(NewPersonName))
            {
                _person.PersonName = NewPersonName;
                string appDataPath = FileSystem.AppDataDirectory;
                string personsFromFile = File.ReadAllText(Path.Combine(appDataPath, $"{className}_class.txt"));
                string[] separatedPersons = personsFromFile.Split("\r\n");
                separatedPersons = separatedPersons.Skip(1).ToArray();


                string newWholeClass = $"{this.ClassName}";
                foreach (string PersonLine in separatedPersons)
                {
                    string[] things = PersonLine.Split(';');

                    if (things[0] != personName)
                    {
                        newWholeClass += $"\r\n{things[0]};true";
                    }
                    else if(things[0] == personName)
                    {
                        newWholeClass += $"\r\n{NewPersonName};true";
                    }
                }

                File.Delete(Path.Combine(FileSystem.AppDataDirectory + $"{this.ClassName}_class.txt"));
                File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, $"{this.ClassName}_class.txt"), newWholeClass);
            }
        }

    }
    public void DeletePerson_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var uczen = (Person)button.BindingContext;

        string appDataPath = FileSystem.AppDataDirectory;
        string personsFromFile = File.ReadAllText(Path.Combine(appDataPath, $"{this.className}_class.txt"));
        string[] separatedPersons = personsFromFile.Split("\r\n");
        separatedPersons = separatedPersons.Skip(1).ToArray();


        string newWholeClass = $"{this.ClassName}";
        foreach (string PersonLine in separatedPersons)
        {
            string[] things = PersonLine.Split(';');

            if (things[0] != uczen.PersonName)
            {
                newWholeClass += $"\r\n{things[0]};true";
            }
            else continue;
        }

        File.Delete(Path.Combine(FileSystem.AppDataDirectory + $"{this.ClassName}_class.txt"));
        File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, $"{this.ClassName}_class.txt"), newWholeClass);
        Persons.Clear();
        LoadClass(this.className);

    }
}