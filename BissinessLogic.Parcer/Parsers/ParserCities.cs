using System;
using System.Collections.Generic;
using BusinessLogic.Database.Interfaces;
using BusinessLogic.Parser.Services.Interfaces;

namespace BusinessLogic.Parser.Parsers
{
    class ParserCities
    {
        private readonly ICityService _cityService;
        private readonly ICityWebDataService _cityWebDataService;
        
        public ParserCities(ICityService cityService, ICityWebDataService cityWebDataService)
        {
            _cityService = cityService;
            _cityWebDataService = cityWebDataService;
        }

        public async void Start()
        {
            Console.WriteLine("Start City parser");

            List<string> citiesList = new List<string>()
            {
                "Minsk",
                "Brest",
                "Grodno",
                "Gomel",
                "Vitebsk",
                "Mogilev",
                //"Bobrujsk",
                //"Baranovichi",
                //"Novopolock",
                //"Pinsk",
                //"Borisov",
                //"Lida",
                //"Mozyr",
                //"Polock",
                //"Slonim",
                //"Orsha",
                //"Molodechno",
                //"Zhlobin",
                //"Kobrin",
                //"Sluck"
            };

            var cities = _cityWebDataService.GetData(selector: ".//*/li/select/option", url: @"https://m.select.by/kurs");
            foreach (var city in cities)
            {
                if (citiesList.Contains(city.NameLat)) 
                {
                    await _cityService.Add(city); 
                }
            }
        }
    }
}
