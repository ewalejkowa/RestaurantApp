﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace serwer2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RestaurantEntities3 : DbContext
    {
        public RestaurantEntities3()
            : base("name=RestaurantEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ELEMENT_ZAMOWIENIA> ELEMENT_ZAMOWIENIA { get; set; }
        public virtual DbSet<KATEGORIA> KATEGORIA { get; set; }
        public virtual DbSet<MENU> MENU { get; set; }
        public virtual DbSet<POZYCJA_MENU> POZYCJA_MENU { get; set; }
        public virtual DbSet<PRACOWNIK> PRACOWNIK { get; set; }
        public virtual DbSet<REALIZACJA_ZAMOWIENIA> REALIZACJA_ZAMOWIENIA { get; set; }
        public virtual DbSet<STATUS> STATUS { get; set; }
        public virtual DbSet<ZAMOWIENIE> ZAMOWIENIE { get; set; }
    }
}
