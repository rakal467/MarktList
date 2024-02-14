using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketList;
using MarketList.Model;
using MarketList.Tools;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MarketList.MainPage;

namespace MarketList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(CategoryName), "CategoryName")]  //Свойство запроса, CategoryName - это имя свойства. Идентификатор параметра запроса.
    public partial class CategoryList : ContentPage
    {
        public Category _category;
        public string categoryName;

        public string CategoryName
        {
            get => categoryName; //Метод доступа в C# - это часть свойства (property), которая определяет, как можно получить (через get) и установить (через set) значение свойства.
            set
            {
                categoryName = value;
                Category category = App.DB.GetCategoryByName(CategoryName);
                _category = category;
                AllItem.ItemsSource = new ObservableCollection<Housing>(App.DB.GetHousings().Where(s => s.IdCategory == _category.ID));

            }
        }





        public CategoryList()
        {

            InitializeComponent();



        }
        //private void BtnAdd(object sender, EventArgs e)
        //{
        //    Category category = new Category { Name = AddEntry.Text };
        //    App.DB.EditCategory(category);

        //    OnAppearing();
        //    messagelabel.Text = "Добавлено!";
        //}


        private void BtnAdd(object sender, EventArgs e)
        {

            Housing housing = new Housing { Name = AddEntry.Text, IdCategory = _category.ID };
            App.DB.EditHousing(housing);

            OnAppearing();
            messagelabel.Text = "Добавлено!";
        }

        private void BtnEdit(object sender, EventArgs e)
        {
            if (AllItem.SelectedItem != null)
            {
                Housing selectedHousing = AllItem.SelectedItem as Housing;
                if (AddEntry.Text != null)
                {
                    selectedHousing.Name = AddEntry.Text;

                    AllItem.ItemsSource = new ObservableCollection<Housing>(App.DB.GetHousings());

                    OnAppearing();
                    messagelabel.Text = "Сохранено!!!";
                }
                else
                {
                    messagelabel.Text = "Введите Нахвание!!!";
                }
            }
            else
            {
                messagelabel.Text = "Выберите Продукт!!!";
            }
        }

        protected override void OnAppearing()
        {
            AllItem.ItemsSource = new ObservableCollection<Housing>(App.DB.GetHousings().Where(s => _category != null && s.IdCategory == _category.ID));
        }

        private void BtnDel(object sender, EventArgs e)
        {
            if (AllItem.SelectedItem != null)
            {
                Housing selectedHousings = AllItem.SelectedItem as Housing;
                App.DB.DeleteHousing(selectedHousings);
                OnAppearing();
                messagelabel.Text = "Удалено!!!";
            }
            else
            {
                messagelabel.Text = "Выберите Продукт для удаления";
            }
        }

        private void AllItem_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (AllItem.SelectedItem != null)
            {
                AddEntry.Text = ((Housing)AllItem.SelectedItem).Name;
            }
        }
    }
}
