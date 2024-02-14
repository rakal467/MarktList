using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MarketList.Model;
using Xamarin.Forms;

namespace MarketList.Tools
{
    public class DataBase
    {
        private const string FILE_NAME = "db.db";
        public DboContext dB;

        public DboContext DB
        {
            get
            {
                if (dB == null)
                {
                    string path = DependencyService.Get<IDBPath>().GetDBPath(FILE_NAME);
                    dB = new DboContext(path);
                    dB.Database.EnsureCreated();
                }
                return dB;
            }
        }

        public DataBase()
        {

            List<Category> categories = new List<Category>
            {
                new Category { ID = 1, Name = "Премиум" },
                new Category { ID = 2, Name = "Комфорт" },
                new Category { ID = 3, Name = "Эконом" }
            };

            List<Housing> housings = new List<Housing>
            {
                new Housing { ID = 1, Name = "Котедж", IdCategory = 1 },
                new Housing { ID = 2, Name = "Квартира 150 кв", IdCategory = 1 },
                new Housing { ID = 3, Name = "Вилла", IdCategory = 1 },
                new Housing { ID = 4, Name = "Квартира 100 кв", IdCategory = 2 },
                new Housing { ID = 5, Name = "Квартира 90 кв ", IdCategory = 2 },
                new Housing { ID = 6, Name = "Квартира 70 кв ", IdCategory = 2 },
                new Housing { ID = 7, Name = "Квартира 50 кв ", IdCategory = 3 },
                new Housing { ID = 8, Name = "Квартира 40 кв ", IdCategory = 3 },
                new Housing { ID = 9, Name = "Квартира 30 кв ", IdCategory = 3 }
            };
        }

       
        public Category GetByCategoryID(int id)
        {
            return DB.Categories.FirstOrDefault(c => c.ID == id);
        }
        public Housing GetByHousingsID(int id)
        {
            return DB.Housing.FirstOrDefault(c => c.ID == id);
        }
        public Category GetCategoryByName(string categoryName)
        {

            return DB.Categories.FirstOrDefault(c => c.Name == categoryName);
            
        }

        public void EditHousing(Housing housing)
        {
            if (housing.ID == 0)
            {
                DB.Housing.Add(housing);
            }
            else
            {
                Housing housing1 = GetByHousingsID(housing.ID);
                DB.Entry(housing1).CurrentValues.SetValues(housing);
            }
            DB.SaveChanges();

           
        }

        public ObservableCollection<Category> GetCategories()
        {
            return new ObservableCollection<Category>(DB.Categories.ToList());

        }

        public ObservableCollection<Housing> GetHousings()
        {
            return new ObservableCollection<Housing>(DB.Housing.ToList());
        }
        public Category DeleteCategory(Category category)
        {

            if (category != null)
            {
                DB.Categories.Remove(category);
            }
            DB.SaveChanges();
            return category;
        }

        public void DeleteHousing(Housing housing)
        {

            if (housing != null)
            {
                DB.Housing.Remove( housing );
            }
           DB.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            if(category.ID == 0)
            {   
                DB.Categories.Add(category);
            }
            else
            {
                Category category1 = GetByCategoryID(category.ID);
                DB.Entry(category1).CurrentValues.SetValues(category);
            }
            DB.SaveChanges();
           
        }

    }
}
