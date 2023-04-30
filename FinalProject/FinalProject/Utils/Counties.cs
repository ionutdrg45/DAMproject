using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FinalProject.Utils
{
    public class Counties
    {
        public static ObservableCollection<County> GetCounties()
        {
            ObservableCollection<County> countiesList = new ObservableCollection<County>
            {
                new County { Name = "Alba" },
                new County { Name = "Arad" },
                new County { Name = "Arges" },
                new County { Name = "Bacau" },
                new County { Name = "Bihor" },
                new County { Name = "Bistrita-Nasaud" },
                new County { Name = "Botosani" },
                new County { Name = "Braila" },
                new County { Name = "Brasov" },
                new County { Name = "Bucuresti" },
                new County { Name = "Buzau" },
                new County { Name = "Calarasi" },
                new County { Name = "Caras-Severin" },
                new County { Name = "Cluj" },
                new County { Name = "Constanta" },
                new County { Name = "Covasna" },
                new County { Name = "Dambovita" },
                new County { Name = "Dolj" },
                new County { Name = "Galati" },
                new County { Name = "Giurgiu" },
                new County { Name = "Gorj" },
                new County { Name = "Harghita" },
                new County { Name = "Hunedoara" },
                new County { Name = "Ialomita" },
                new County { Name = "Iasi" },
                new County { Name = "Ilfov" },
                new County { Name = "Maramures" },
                new County { Name = "Mehedinti" },
                new County { Name = "Mures" },
                new County { Name = "Neamt" },
                new County { Name = "Olt" },
                new County { Name = "Prahova" },
                new County { Name = "Salaj" },
                new County { Name = "Satu Mare" },
                new County { Name = "Sibiu" },
                new County { Name = "Suceava" },
                new County { Name = "Teleorman" },
                new County { Name = "Timis" },
                new County { Name = "Tulcea" },
                new County { Name = "Valcea" },
                new County { Name = "Vaslui" },
                new County { Name = "Vrancea" }
            };

            return countiesList;
        }
    }
}
