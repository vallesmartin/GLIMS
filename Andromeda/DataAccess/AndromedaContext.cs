using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApp.DataAccess
{
    /*
        El contexto de la BBDD.
        Reemplazar en Web.config 
        <connectionStrings>
	      <add name="AndromedaContext" connectionString="data source=<NombreDelServidor>\<NombreInstanciaSQL>; initial catalog=<NombreDeLaBBDD>;persist security info=True;user id=<usuarioSQL>;password=<passwordSQL>;" providerName="System.Data.SqlClient" />
        </connectionStrings>
    */
    public class AndromedaContext : DbContext
    {
        public AndromedaContext() : base("AndromedaContext") { }

        // Esquemas de tablas
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<EntityModel> Entities { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }
        public DbSet<DocumentLineModel> DocumentLines { get; set; }
        public DbSet<NumeratorsModel> Numerator { get; set; }
        public DbSet<InvModel> Inventory { get; set; }
        public DbSet<InvLineModel> InventoryLine { get; set; }
        public DbSet<ChangeModel> Changes { get; set; }
        public DbSet<LogModel> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Aca se cambia la definicion del nombre del <modelo> por "tabla"
            modelBuilder.Entity<UserModel>().ToTable("Users");
            modelBuilder.Entity<UserRoleModel>().ToTable("UserRoles");
            modelBuilder.Entity<CategoryModel>().ToTable("Categories");
            modelBuilder.Entity<EntityModel>().ToTable("Entities");
            modelBuilder.Entity<ItemModel>().ToTable("Items");
            modelBuilder.Entity<DocumentModel>().ToTable("Documents");
            modelBuilder.Entity<DocumentLineModel>().ToTable("DocumentLines");
            modelBuilder.Entity<NumeratorsModel>().ToTable("Numerator");
            modelBuilder.Entity<InvModel>().ToTable("Inv");
            modelBuilder.Entity<InvLineModel>().ToTable("InvLines");
            modelBuilder.Entity<ChangeModel>().ToTable("Changes");
            modelBuilder.Entity<LogModel>().ToTable("Logs");

            base.OnModelCreating(modelBuilder);
        }
    }

    // Enumeracion de los objetos tabla
    public static class AndromedaContextObject
    {
        public static string OBJ_DOCUMENTS = "Documents";
        public static string OBJ_DOCUMENTLINES = "DocumentLines";
        public static string OBJ_ITEMS = "Items";
        public static string OBJ_ENTITY = "Entity";
        public static string OBJ_USERS = "Users";
        public static string OBJ_USERROLES = "UserRoles";
    }
}