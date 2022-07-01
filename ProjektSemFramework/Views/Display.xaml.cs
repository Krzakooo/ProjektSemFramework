using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace ProjektSemFramework.Views
{
    /// <summary>
    /// Interaction logic for Display.xaml
    /// </summary>
    public partial class Display : Page
    {
        public Display()
        {
            InitializeComponent();

           
        } 


        private void BtnClick1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Order());
        }

        private void BtnClickLoad(object sender, RoutedEventArgs e)
        {
            TranslatorsDBEntities db = new TranslatorsDBEntities();
            var Ts = from d in db.Tlumaczes
                     join b in db.Jezyki_Tlumacza on d.id_tlumacza equals b.id_tlumacza
                     join a in db.Jezykis on b.id_jezyka equals a.id_jezyka
                     select new
                     {
                         Translator = d.imie + " " + d.nazwisko,
                         NativeSpeaker = d.jezyk_ojczysty,
                         Language = a.jezyk
                     };



            foreach (var item in Ts)
            {
                //Console.WriteLine(item.nazwisko);
            }

            this.TranslatorGrid.ItemsSource = Ts.ToList();
        }

        private void BtnClickAdd(object sender, RoutedEventArgs e)
        {
            TranslatorsDBEntities db = new TranslatorsDBEntities();
            Tlumacze tlumaczeObject = new Tlumacze()
            {
                imie = txtName.Text,
                nazwisko = txtSurame.Text,
                jezyk_ojczysty = txtMt.Text,
                telefon = "213721370"
            };

            decimal n;
            try
            {
                // Do not initialize this variable here.
                n = Decimal.Parse(txtPrice.Text);
            }
            catch
            {
                n = 50; 
            }
            Jezyki jezykiObject = new Jezyki()
            {
                jezyk = txtLang.Text,
                cena_za_strone = n
                
            };
            Jezyki_Tlumacza jezykiTlumaczaObject = new Jezyki_Tlumacza()
            {
                id_jezyka = jezykiObject.id_jezyka,
                id_tlumacza = tlumaczeObject.id_tlumacza,

            };

            db.Tlumaczes.Add(tlumaczeObject);
            db.Jezykis.Add(jezykiObject);
            db.Jezyki_Tlumacza.Add(jezykiTlumaczaObject);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}
