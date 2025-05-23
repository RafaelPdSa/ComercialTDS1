using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercialTDSClass
{
    public class Categoria
    {

        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Sigla { get; set; }

        public Categoria() { }

        public Categoria(int? id, string? nome, string? sigla)
        {
            Id = id;
            Nome = nome;
            Sigla = sigla;
        }
        public Categoria(string? nome, string? sigla)
        {
            Nome = nome;
            Sigla = sigla;
        }
        // inserir, atualizar, listar, obterPorId(id)
        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_categoria_insert";
            //cmd.Parameters.Add("spnome",MySqlDbType.VarChar).Value = Nome;
            cmd.Parameters.AddWithValue("spnome", Nome);
            cmd.Parameters.AddWithValue("spsigla", Sigla);
            Id = Convert.ToInt32(cmd.ExecuteNonQuery());
            cmd.Connection.Close();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Categoria ObterPorId(int id)
        {
            Categoria categoria = new();
            // código do método
            var cmd = Banco.Abrir();
            cmd.CommandText = $"select * from niveis where id = {id}";
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //nivel.Id = dr.GetInt32(0);
                //nivel.Nome = dr.GetString(1);
                //nivel.Sigla = dr.GetString(2);
                categoria = new(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
            }
            dr.Close();
            cmd.Connection.Close();
            return categoria;
        }
        public static List<Categoria> ObterLista()
        {
            List<Categoria> categoria = new();
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from categorias order by nome";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                categoria.Add(new(dr.GetInt32(0), dr.GetString(1), dr.GetString(2)));
            }
            dr.Close();
            cmd.Connection.Close();
            return categoria;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Atualizar()
        {
            // como este método não é estático, precisamos considerar que 
            // as propriedades já possuem valores atribuídos
            bool atualizado = false;
            if (Id < 1) return atualizado;
            var cmd = Banco.Abrir();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_categoria_update";
            cmd.Parameters.AddWithValue("spid", Id);
            cmd.Parameters.AddWithValue("spnome", Nome);
            cmd.Parameters.AddWithValue("spsigla", Sigla);
            if (cmd.ExecuteNonQuery() > 0)
            {
                atualizado = true;
            }
            cmd.Connection.Close();
            return atualizado;
        }

    }
}
