using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oef13_8_Personen
{
    public class Persoon
    {
        public Persoon(string naam,
                       string voornaam,
                       string adres,
                       DateTime geb,
                       string tel,
                       GeslachtEnum g)
        {
            Naam = naam;
            Voornaam = voornaam;
            GeboorteDatum = geb;
            Adres = adres;
            Telefoon = tel;
            Geslacht = g;
        }

        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Adres { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Telefoon { get; set; }
        public GeslachtEnum Geslacht { get; set; }

        public override string ToString()
        {
            return Naam + " " + Voornaam;
        }
    }

    public enum GeslachtEnum
    {
        M,
        V
    }
}
