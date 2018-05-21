namespace CupsApp.Migrations
{
    using CupsApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CupsApp.Context.CupContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CupsApp.Context.CupContext";
        }

        protected override void Seed(CupsApp.Context.CupContext context)
        {
        }
    }
}
