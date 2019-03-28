namespace Desafio3A.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ListaCompra")]
    public partial class ListaCompra
    {
        public int Id { get; set; }

        public int IdAlimento { get; set; }

        public int Quantidade { get; set; }

        public virtual Alimento Alimento { get; set; }
    }
}
