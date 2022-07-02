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
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public Order()
        {
            InitializeComponent();
        }

        private void BtnClick1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Display());
        }

        private void BtnClickLoad(object sender, RoutedEventArgs e)
        {
            TranslatorsDBEntities db = new TranslatorsDBEntities();
            var Ts = from d in db.Dokumenties
                     join k in db.Kliencis on d.id_klienta equals k.id_klienta
                     join j in db.Jezykis on d.id_jezyka_oryginalu equals j.id_jezyka
                     join t in db.Tlumaczes on d.id_tlumacza equals t.id_tlumacza
                     select new
                     {
                         Customer = k.imie + " " + k.nazwisko,
                         Translator = t.imie + " " + t.nazwisko,
                         OriginalLanguage = d.id_jezyka_oryginalu,
                         TranslationLanguage = d.id_jezyka_tlumaczenia,
                         isTranslated = d.przetlumaczone,
                         isDelivered = d.oddane_klientowi
                     };



            foreach (var item in Ts)
            {
                //Console.WriteLine(item.nazwisko);
            }

            this.TranslationsGrid.ItemsSource = Ts.ToList();
        }

        private void BtnClickAdd(object sender, RoutedEventArgs e)
        {
            TranslatorsDBEntities db = new TranslatorsDBEntities();
            int n;
            try
            {
                // Do not initialize this variable here.
                n = Int32.Parse(txtPhone.Text);
            }
            catch
            {
                n = 000000000;
            }

            if (db.Jezykis.Any(o => o.jezyk == txtLangO.Text) && db.Jezykis.Any(o => o.jezyk == txtLangT.Text)) 
            {
                var orig = db.Jezykis
                      .Where(o => o.jezyk == txtLangO.Text)
                      .FirstOrDefault();

                var trans = db.Jezykis
                            .Where(t => t.jezyk == txtLangT.Text)
                            .FirstOrDefault();

                var origTranslators = from o in db.Jezyki_Tlumacza
                                      where o.id_jezyka == orig.id_jezyka
                                      select o;

                var transTranslators = from t in db.Jezyki_Tlumacza
                                       where t.id_jezyka == trans.id_jezyka
                                       select t;

                int destId = 999;
                foreach (var item in origTranslators)
                {
                    foreach (var item2 in transTranslators)
                    {
                        if (item.id_tlumacza == item2.id_tlumacza)
                        {
                            destId = item.id_tlumacza;
                        }
                    }
                }
                if (destId == 999)
                {
                    MessageBox.Show("No suitable translator!");
                }
                else
                {
                    var translator = db.Tlumaczes
                            .Where(d => d.id_tlumacza == destId)
                            .FirstOrDefault();

                    Klienci klienciObject = new Klienci()
                    {
                        imie = txtName.Text,
                        nazwisko = txtSurame.Text,
                        telefon = n.ToString(),
                        kraj = "default",
                        miasto = "default",
                        ulica = "default",
                        nr_budynku = "0",
                        kod_pocztowy = "00-000"
                    };

                    Dokumenty dokuObject = new Dokumenty()
                    {
                        id_klienta = klienciObject.id_klienta,
                        id_tlumacza = destId,
                        id_jezyka_oryginalu = orig.id_jezyka,
                        id_jezyka_tlumaczenia = trans.id_jezyka,
                        przetlumaczone = "nie",
                        oplacone = "nie"
                    };

                    /*
                    Jezyki jezykiObject = new Jezyki()
                    {
                        jezyk = txtLangO.Text,
                        cena_za_strone = 50

                    };
                    */

                    db.Kliencis.Add(klienciObject);
                    db.Dokumenties.Add(dokuObject);

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
            } else
            {
                MessageBox.Show("These languages are not supported!");
               
            }

           
            
        }
    }
}
