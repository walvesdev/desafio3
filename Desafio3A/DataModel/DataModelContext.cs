namespace Desafio3A.DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataModelContext : DbContext
    {
        public DataModelContext()
            : base("name=DataModel")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Alimento> Alimento { get; set; }
        public virtual DbSet<ListaCompra> ListaCompra { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alimento>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Alimento>()
                .Property(e => e.Preco)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Alimento>()
                .HasMany(e => e.ListaCompra)
                .WithRequired(e => e.Alimento)
                .HasForeignKey(e => e.IdAlimento)
                .WillCascadeOnDelete(false);
        }
    }
}
