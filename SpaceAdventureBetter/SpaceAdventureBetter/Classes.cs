using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventureBetter
{
    public class Planet
    {
        public Planet()
        {

        }
        public Planet(Image image)
        {
            this.name = $"Планета №{PlanetList.Count + 1}";
            this.picurebox.Image = image;
            this.picurebox.SizeMode = PictureBoxSizeMode.StretchImage;
            PlanetList.Add(this);
        }
        public static List<Planet> PlanetList = new List<Planet>();
        public PictureBox picurebox = new PictureBox();
        public List<Company> CompanyForPlanet = new List<Company>();
        public List<Planet> wayOnPlanet = new List<Planet>();
        public string name { get; set; } = "Some";
        public bool isActive { get; set; } = false;
        public bool isUnChecked { get; set; } = true;
        public bool isEndPlanet { get; set; } = false;
        public int way { get; set; } = int.MaxValue;

        public void AddCompany(Company company)
        {
            CompanyForPlanet.Add(company);
            company.PlanetForCompany.Add(this);
        }
        public void RemoveAllCompany()
        {
            for (int i = 0; i < CompanyForPlanet.Count; i++)
            {
                CompanyForPlanet[i].PlanetForCompany.Remove(this);
            }
            CompanyForPlanet.Clear();
        }
        public bool ExistCompany(Company company)
        {
            for (int i = 0; i < CompanyForPlanet.Count; i++)
            {
                if (CompanyForPlanet[i] == company)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Company
    {
        public Company()
        {
            this.name = $"{CompanyList.Count + 1}";
            Random rnd = new Random();
            this.picurebox.BackColor = Color.FromArgb(255, rnd.Next(0, 150), rnd.Next(0, 200), rnd.Next(0, 150));
            CompanyList.Add(this);
        }
        public static List<Company> CompanyList = new List<Company>();
        public PictureBox picurebox = new PictureBox();
        public List<Planet> PlanetForCompany = new List<Planet>();
        public string name { get; set; }
        public bool isActive { get; set; } = false;
        public void AddPlanet(Planet planet)
        {
            PlanetForCompany.Add(planet);
            planet.CompanyForPlanet.Add(this);
        }
        public bool ExistPlanet(Planet planet)
        {
            for (int i = 0; i < PlanetForCompany.Count; i++)
            {
                if (PlanetForCompany[i] == planet)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
