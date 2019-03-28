using Desafio3A.DataModel;
using Desafio3A.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desafio3A.Controllers
{
    public class ListaCompraController : Controller
    {
        DataModelContext db = new DataModelContext();

        public JsonResult BuscarAlimentos(string termo)
        {
            termo = termo ?? "";

            var alimentos = db.Alimento.Where(w => w.Nome.Contains(termo)).ToList();
            var alimentosViewModel = alimentos.Select(s => new ListaCompraVM()
            {
                IdAlimento = s.Id,
                Nome = s.Nome,
                Preco = s.Preco,
                Quantidade = 1
            });

            return Json(alimentosViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarListaCompra()
        {
            var lista = db.ListaCompra
                 .Include(nameof(ListaCompra.Alimento))
                 .ToList()
                 .Select(s => new ListaCompraVM()
                 {
                     Id = s.Id,
                     IdAlimento = s.IdAlimento,
                     Nome = s.Alimento.Nome,
                     Preco = s.Alimento.Preco,
                     Quantidade = s.Quantidade
                 });

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Salvar(List<ListaCompra> dadosWeb)
        {
            var itensListaBanco = db.Set<ListaCompra>().ToList();

            foreach (var item in dadosWeb)
            {
                var itemLista = itensListaBanco.FirstOrDefault(f => f.Id == item.Id);
                if (itemLista == null)
                {
                    var novoItemLista = new ListaCompra();
                    novoItemLista.IdAlimento = item.IdAlimento;
                    novoItemLista.Quantidade = item.Quantidade;
                    db.ListaCompra.Add(novoItemLista);
                }
            }

            foreach (var listItem in itensListaBanco)
            {

                var itemLista = dadosWeb.FirstOrDefault(f => f.Id == listItem.Id);

                if (itemLista != null)
                {
                    db.Entry<ListaCompra>(listItem).CurrentValues.SetValues(itemLista);
                }
            }

            foreach (var item in itensListaBanco)
            {
                var itemLista = dadosWeb.FirstOrDefault(f => f.Id == item.Id);
                if (itemLista == null)
                {
                    db.Entry<ListaCompra>(item).State = System.Data.Entity.EntityState.Deleted;
                }
            }

            db.SaveChanges();

            return Json(new { });
        }
    }
}