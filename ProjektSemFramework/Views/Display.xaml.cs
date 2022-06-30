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

            var Ls = from d in db.Tlumaczes
                     select new
                     {
                         Languages = d.Jezyki_Tlumacza
                     };

            foreach (var item in Ts)
            {
                //Console.WriteLine(item.nazwisko);
            }

            this.TranslatorGrid.ItemsSource = Ts.ToList(); 
        }


        private void BtnClick1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Order());
        }

        private void BtnClickAdd(object sender, RoutedEventArgs e)
        {
            TranslatorsDBEntities db = new TranslatorsDBEntities();
            Tlumacze tlumaczeObject = new Tlumacze()
            {
                imie = txtName.Text,
                nazwisko = txtSurame.Text,
                jezyk_ojczysty = txtLang.Text,
            };

            db.Tlumaczes.Add(tlumaczeObject);
            db.SaveChanges();
        }
    }
}
