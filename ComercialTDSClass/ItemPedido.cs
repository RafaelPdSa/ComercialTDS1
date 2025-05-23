using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ComercialTDSClass
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int PedidoId {get; set;}
        public Produto Produto { get; set; }
        public double ValorUnit { get; set; }//ja tem valor no produto mas pra q colocar n item de pedido?pq o produto tem desconto e  o itemdepedido tbm tem um diferente
        public double Quantidade { get; set; }//double posso usar pra fração por exemplo venda de um produto em fração.. ração e etc.
        public double Desconto { get; set; } // desconto de grana a unica coisa q usa percetual é na classe de desconto; o valor gravado aqui é o valor em real, de acordo com classe de desconto

        public ItemPedido() 
        {
            Produto = new();
        }
        public ItemPedido(int id, int pedidoId, Produto produto, double valorUnit, double quantidade, double desconto)
        {
            Id = id;
            PedidoId = pedidoId;
            Produto = produto;
            ValorUnit = valorUnit;
            Quantidade = quantidade;
            Desconto = desconto;
        }

        public ItemPedido(int pedidoId, Produto produto, double quantidade, double desconto)
        {
            PedidoId = pedidoId;
            Produto = produto;
            Quantidade = quantidade;
            Desconto = desconto;
        }

        public ItemPedido(int id, double quantidade, double desconto)
        {
            Id = id;
            Quantidade = quantidade;
            Desconto = desconto;
        }

        public void Deletar(int id)
        {
            //var cmd = Banco.Abrir();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "sp_item_pedido_delete";
            //cmd.Parameters.AddWithValue("spid", Id);
            //cmd.ExecuteNonQuery;
            var item = ObterPorId(id);
            var cmd = Banco.Abrir();
            cmd.CommandTet = $"update estoques" + $"set quantidade = quantidade + {item.Quantidade}" +
                $ 
            ;

        }

        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_itempedido_insert";
            cmd.Parameters.AddWithValue("sppedido_id", PedidoId);
            cmd.Parameters.AddWithValue("spproduto_id", Produto.Id);
            cmd.Parameters.AddWithValue("spquantidade", Quantidade);
            cmd.Parameters.AddWithValue("spdesconto", Desconto);
        }
        public bool Atualizar()
        {
            return true;
        }
        public static ItemPedido ObterPorId(int id)//id do item de pedido
        {
            ItemPedido itemPedido = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from itempedido where id = {id}";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                itemPedido = new(
                    dr.GetInt32(0),
                    dr.GetInt32(1),
                    Produto.ObterPorId(dr.GetInt32(2)),
                    dr.GetDouble(3),
                    dr.GetDouble(4),
                    dr.GetDouble(5)
                    );
            }
            dr.Close();

            return itemPedido;
        }

        public static List<ItemPedido> ObterListaPorPedidoId(int pedidoId) 
        {
            List<ItemPedido> items = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = "$"
        }


    }
}
