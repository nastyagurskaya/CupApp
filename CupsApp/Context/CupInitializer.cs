using CupsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace CupsApp.Context
{
    public class CupInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CupContext>
    {
        protected override void Seed(CupContext context)
        {
            RegionInfo countryReg = new RegionInfo(new CultureInfo("en-US", false).LCID);
            List<string> countriesNames = new List<string>();
            foreach (CultureInfo cul in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                countryReg = new RegionInfo(new CultureInfo(cul.Name, false).LCID);
                string countryName= countryReg.DisplayName.ToString();
                if(!countriesNames.Contains(countryName))
                    countriesNames.Add( countryName );
            }
            countriesNames.Sort();

            countriesNames.ForEach(c => context.Countries.Add(new Country { CountryName = c}));
            context.SaveChanges();
        }
    }
}