﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationDb2Vto3V
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class aimpEntities : DbContext
    {
        public aimpEntities()
            : base("name=aimpEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ> БАНКИ_ДЛЯ_ОТЧЁТЫ_КЛИЕНТОВ { get; set; }
        public virtual DbSet<ВИДЫ_ТРАНСПОРТА> ВИДЫ_ТРАНСПОРТА { get; set; }
        public virtual DbSet<ГОРОДА> ГОРОДА { get; set; }
        public virtual DbSet<КАТЕГОРИИ_ТРАНСПОРТА> КАТЕГОРИИ_ТРАНСПОРТА { get; set; }
        public virtual DbSet<КОНТРАГЕНТЫ> КОНТРАГЕНТЫ { get; set; }
        public virtual DbSet<КРЕДИТОРЫ> КРЕДИТОРЫ { get; set; }
        public virtual DbSet<МАРКИ> МАРКИ { get; set; }
        public virtual DbSet<МОДЕЛИ> МОДЕЛИ { get; set; }
        public virtual DbSet<ОТЧЁТЫ_КЛИЕНТОВ> ОТЧЁТЫ_КЛИЕНТОВ { get; set; }
        public virtual DbSet<ПОЛЬЗОВАТЕЛИ> ПОЛЬЗОВАТЕЛИ { get; set; }
        public virtual DbSet<РЕГИОНЫ> РЕГИОНЫ { get; set; }
        public virtual DbSet<РЕКВИЗИТЫ> РЕКВИЗИТЫ { get; set; }
        public virtual DbSet<СДЕЛКИ> СДЕЛКИ { get; set; }
        public virtual DbSet<спр_БАНКИ_ОТЧЁТЫ_КЛИЕНТОВ> спр_БАНКИ_ОТЧЁТЫ_КЛИЕНТОВ { get; set; }
        public virtual DbSet<спр_ПРОГРАММЫ_КРЕДИТОВАНИЯ> спр_ПРОГРАММЫ_КРЕДИТОВАНИЯ { get; set; }
        public virtual DbSet<спр_СТАТУСЫ_БАНКА> спр_СТАТУСЫ_БАНКА { get; set; }
        public virtual DbSet<спр_СТАТУСЫ_КЛИЕНТОВ> спр_СТАТУСЫ_КЛИЕНТОВ { get; set; }
        public virtual DbSet<ТИПЫ_ДВИГАТЕЛЕЙ> ТИПЫ_ДВИГАТЕЛЕЙ { get; set; }
        public virtual DbSet<ТИПЫ_ШАБЛОНОВ> ТИПЫ_ШАБЛОНОВ { get; set; }
        public virtual DbSet<ТРАНСПОРТ> ТРАНСПОРТ { get; set; }
        public virtual DbSet<ШАБЛОНЫ> ШАБЛОНЫ { get; set; }
    }
}
